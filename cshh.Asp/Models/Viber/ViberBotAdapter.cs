using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using ViberBot;
using ViberBot.Model.Request;

namespace cshh.Asp.Models.Viber
{
    public class ViberBotAdapter : Model.Services.Viber.IViberBot, IDisposable
    {        
        Lazy<ViberBot.ViberBotClient> _botClient; 
        public ViberBotAdapter(Func<ViberBot.ViberBotClient> botClientFactory)
        {
            _botClient = new Lazy<ViberBotClient>(botClientFactory);            
        }

        public void Dispose()
        {
            if(_botClient.IsValueCreated)
                _botClient.Value.Dispose();
        }

        public Task<HttpResponseMessage> SendMessage(string from, string to, string msg)
        {           
            TextMessage textMessage = new ViberBot.Model.Request.TextMessage(msg, from, to);
            Task<HttpResponseMessage> sendMessage = _botClient.Value.SendMessage(textMessage);
            
            return sendMessage;
        }
    }
}