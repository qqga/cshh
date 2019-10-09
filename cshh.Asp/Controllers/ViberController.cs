using cshh.Asp.Extensions;
using cshh.Asp.Models.Viber;
using cshh.Data.Services.DbContexts;
using cshh.Data.Services.Repo;
using cshh.Data.Viber;
using cshh.Model.Services.Viber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace cshh.Asp.Controllers
{
    public class ViberController : Controller
    {
        private readonly IViberService _viberService;
        readonly IUserRepository _UserRepository;

        public ViberController(IViberService viberService, IUserRepository userRepository)
        {

            #region RegionName
            //CommonDbContext commonDbContext = new CommonDbContext();
            //List<ViberUser> list = commonDbContext.ViberUsers.ToList();
            #endregion
            _UserRepository = userRepository;
            _viberService = viberService;
        }

        // GET: Viber
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Event()
        {
            string content;
            using(var reader = new System.IO.StreamReader(Request.InputStream))
                content = reader.ReadToEnd();

            //string parameters = string.Join("; ", Request.Params.AllKeys.Select(k => $"[{k}]:'{Request.Params[k]}'"));
            _viberService.AddEvent(content);

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }

        [Authorize(Roles="ViberAdmin")]
        public ActionResult Events()
        {
            var list = _viberService.GetEvents().OrderByDescending(e => e.DateTimeUTC).ToList();

            return View(list);
        }

        [Authorize(Roles = "ViberAdmin")]
        public ActionResult SendMsgForm()
        {
            var userProfile = this.GetUserProfile(_UserRepository);
            IEnumerable<ViberUser> viberUserOfCurrentUser = _viberService.GetUsers().Where(user => user.UserProfile_Id == userProfile.Id).ToList();
            SendMessageViewModel sendMessageViewModel = new Models.Viber.SendMessageViewModel(_viberService.GetUsers().ToList(), viberUserOfCurrentUser);
            return View(sendMessageViewModel);
        }

        [Authorize(Roles = "ViberAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendMsgForm(IList<string> users, string msg, string fromName, string fromId)
        {
            try
            {
                await _viberService.SendMsgFromForm(users, msg, fromName, fromId);
            }
            catch(Exception ex)
            {
                return new AjaxResponse(ex);
            }

            return new AjaxResponse("ok");
        }


        [HttpPost]
        public async Task<ActionResult> SendMsg(string token, string from, string to, string msg)
        {
            await _viberService.SendMsg(token, from, to, msg);
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _viberService.Dispose();
        }
    }
}