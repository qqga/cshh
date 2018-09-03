using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Web;

namespace cshh.Asp.Handlers
{
    public class TaskClientsContainer
    {
        private readonly ConcurrentDictionary<string, TaskClientInfo> Clients = new ConcurrentDictionary<string, TaskClientInfo>();
        public void AddClient(string userAppId, WebSocket socket)
        {
            Clients.AddOrUpdate(userAppId,
                (s) => new TaskClientInfo(userAppId, socket),
                (s, c) => { c.Add(socket); return c; });

        }

        public void Remove(string userAppId, WebSocket socket)
        {
            if(
                Clients.TryGetValue(userAppId, out TaskClientInfo clientInfo) &&
                clientInfo.Remove(socket) &&
                Clients.TryRemove(userAppId, out TaskClientInfo clientInfoRemoved))
            {
                try
                {
                    clientInfoRemoved.Dispose();
                }
                catch(Exception ex)
                {

                }
            }
        }

        public void WithLockSoket(string userAppId, Action<WebSocket> action)
        {
            if(Clients.TryGetValue(userAppId, out TaskClientInfo clientInfo))
            {
                clientInfo.WithLockSoket(action);
            }
        }

    }
}