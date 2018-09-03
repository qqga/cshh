using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Web;

namespace cshh.Asp.Handlers
{
    public class TaskClientInfo
    {
        public DateTime DateTimeCreate { get; } = DateTime.Now;
        public string UserAppId { get; set; }

        List<WebSocket> Sockets = new List<WebSocket>();

        private ReaderWriterLockSlim Locker = new ReaderWriterLockSlim();
        public bool IsEmpty => Sockets == null || Sockets.Count == 0;

        public TaskClientInfo(string userAppId, WebSocket webSocket)
        {
            this.UserAppId = userAppId;
            Locker.EnterWriteLock();
            try
            {
                Sockets.Add(webSocket);
            }
            finally
            {
                Locker.ExitWriteLock();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="socket"></param>
        /// <returns>true if empty now</returns>
        public bool Remove(WebSocket socket)
        {
            if(Sockets.Contains(socket))
            {
                Locker.EnterWriteLock();
                try
                {
                    Sockets.Remove(socket);
                    DisposeSocket(socket);
                }
                finally
                {
                    Locker.ExitWriteLock();
                }
            }
            return IsEmpty;
        }

        public void Add(WebSocket webSocket)
        {
            Locker.EnterWriteLock();
            try
            {
                Sockets.Add(webSocket);
            }
            finally
            {
                Locker.ExitWriteLock();
            }
        }

        public void WithLockSoket(Action<WebSocket> action)
        {
            Locker.EnterReadLock();
            try
            {
                foreach(WebSocket socket in Sockets)
                {
                    try
                    {
                        action(socket);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            finally
            {
                Locker.ExitReadLock();
            }
        }

        public void Dispose()
        {
            Locker.EnterWriteLock();
            try
            {
                foreach(WebSocket webSocket in Sockets)
                {
                    webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Dispose ClientInfo.", CancellationToken.None).Wait();
                    DisposeSocket(webSocket);
                }
            }
            finally
            {
                Locker.ExitWriteLock();
            }
            Locker.Dispose();
        }

        void DisposeSocket(WebSocket socket, bool needThrow = false)
        {
            try
            {
                socket.Dispose();
            }
            catch(Exception ex)
            {
                if(needThrow)
                    throw ex;
            }
        }
    }
}