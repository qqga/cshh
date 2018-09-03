using cshh.Data.Services.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.WebSockets;

namespace cshh.Asp.Handlers
{
    /// <summary>
    /// Сводное описание для TaskHandler
    /// </summary>
    public class TaskHandler : IHttpHandler
    {
        static readonly TaskClientsContainer ClientsContainer = new TaskClientsContainer();

        public void ProcessRequest(HttpContext context)
        {
            if(context.IsWebSocketRequest)
                context.AcceptWebSocketRequest(WebSocketRequest);
        }

        async Task WebSocketRequest(AspNetWebSocketContext context)
        {
            var socket = context.WebSocket;
            string userAppId = context.QueryString["userAppId"];

            if(string.IsNullOrEmpty(userAppId))
            {
                context.WebSocket.Abort();
                return;
            }

            ClientsContainer.AddClient(userAppId, socket);

            await SendInitData(userAppId, socket);

            while(true)
            {
                var buffer = new ArraySegment<byte>(new byte[4096]);

                WebSocketReceiveResult result = null;
                try
                {
                    result = await socket.ReceiveAsync(buffer, CancellationToken.None);
                }
                catch(Exception ex) when(socket.State != WebSocketState.Open)
                {
                    ClientsContainer.Remove(userAppId, socket);
                    return;
                }

                if(result.MessageType == WebSocketMessageType.Close)
                {
                    ClientsContainer.Remove(userAppId, socket);
                    return;
                }
            }
        }

        async Task SendInitData(string userAppId, WebSocket socket)
        {
            Model.Services.Tasks.ITaskService taskService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(Model.Services.Tasks.ITaskService)) as Model.Services.Tasks.ITaskService;
            //taskService.Repository.SetProxyCreationEnabled(false);

            var userTasks = taskService.GetUserTasks(userAppId).ToList();

            //string serializedTasks = Newtonsoft.Json.JsonConvert.SerializeObject(userTasks, typeof(IEnumerable<TaskLib.ITask>), new Newtonsoft.Json.JsonSerializerSettings());
            string serializedTasks = userTasks.ToJson<TaskLib.ITask>();
            string message = $"{{Operation:\"init\",Tasks:{serializedTasks}}}";
            byte[] bytes = UTF8Encoding.Default.GetBytes(message);
            ArraySegment<byte> arraySegment = new ArraySegment<byte>(bytes);
            await socket.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);

        }

        public static void InformClients(string userAppId, string mess)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(mess);
            ArraySegment<byte> arraySegment = new ArraySegment<byte>(bytes);
            ClientsContainer.WithLockSoket(userAppId, (s) => s.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None));
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}