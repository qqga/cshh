using cshh.Data.Services.Repo;
using cshh.Data.Viber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace cshh.Model.Services.Viber
{

    public interface IViberBot : IDisposable
    {
        Task<HttpResponseMessage> SendMessage(string from, string to, string msg);
    }

    public interface IViberService : IDisposable
    {
        void AddEvent(string requestContent);
        IQueryable<ViberEvent> GetEvents();
        IQueryable<ViberUser> GetUsers();
        Task SendMsg(string token, string from, string to, string msg);
        Task SendMsgFromForm(IEnumerable<string> users, string msg, string fromName, string fromId);
    }

    public class ViberService : IViberService
    {
        readonly IViberRepository _viberRepository;
        readonly IViberBot _ViberBot;
        readonly IWorkContext _WorkContext;
        public ViberService(IViberRepository viberRepository, IViberBot viberbot, IWorkContext workContext)
        {
            _WorkContext = workContext;
            _ViberBot = viberbot;
            _viberRepository = viberRepository;
        }


        public IQueryable<ViberEvent> GetEvents() => _viberRepository.ViberEvents;
        public IQueryable<ViberUser> GetUsers() => _viberRepository.ViberUsers;
        public void AddEvent(string requestContent)
        {
            ViberEvent viberEvent = new cshh.Data.Viber.ViberEvent() { DateTimeUTC = DateTime.UtcNow, RequestContent = requestContent/*, Params = parameters*/ };
            _viberRepository.Add(viberEvent, true);
        }

        public async Task SendMsg(string token, string from, string to, string msg)
        {
            if(string.IsNullOrEmpty(token))
                throw new ArgumentNullException(nameof(token));

            ViberUser userByApiToken = _viberRepository.ViberUsers.FirstOrDefault(u => u.ApiToken == token);

            if(userByApiToken == null)
            {
                AddEvent(new { @event = "tokenNotFound", token, from, to, msg }.ToJsonString());
                throw new Exception("User with received apiToken not found.");
            }
            AddEvent(new { @event = "ApiMsg", userName = userByApiToken.Name, UserId = userByApiToken.Id, to, msg }.ToJsonString());
            HttpResponseMessage sendMessage = await _ViberBot.SendMessage(from, to, msg);
            string response = await sendMessage.Content.ReadAsStringAsync();
            AddEvent(new { @event = "ApiMsgResponse", userName = userByApiToken.Name, UserId = userByApiToken.Id, from, to, msg, response }.ToJsonString());
        }

        public async Task SendMsgFromForm(IEnumerable<string> users, string msg, string fromName, string fromId)
        {
            if(users == null)
                throw new ArgumentNullException(nameof(users));
            if(string.IsNullOrEmpty(msg))
                throw new ArgumentNullException(nameof(msg));
            if(string.IsNullOrEmpty(fromName))
                throw new ArgumentNullException(nameof(fromName));
            if(string.IsNullOrEmpty(fromId))
                throw new ArgumentNullException(nameof(fromId));

            ViberUser viberUserId = GetUsers().First(u => u.ViberId == fromId && u.UserProfile.ApplicationUserId == _WorkContext.UserAppId);

            if(viberUserId == null)
                throw new ArgumentException($"Viberuser with id {fromId} not found at current user.");

            IEnumerable<string> realUsersTo = GetUsers().Select(vu => vu.ViberId).Intersect(users).ToList();
            foreach(string realUserTo in realUsersTo)
            {
                await SendMsg(viberUserId.ApiToken, fromName, realUserTo, msg);
            }
        }

        public void Dispose()
        {
            _ViberBot.Dispose();
        }

    }

}