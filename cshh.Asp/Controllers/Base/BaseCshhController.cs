using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cshh.Asp.Extensions;
using cshh.Data.Services.Repo;
using cshh.Data.User;
using Microsoft.AspNet.Identity;
namespace cshh.Asp.Controllers.Base
{
    public class BaseCshhController : Controller
    {
        public BaseCshhController()
        {            
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}

namespace cshh.Asp
{
    public static class ControllerExtensions
    {
        public static string GetAppUserId(this Controller controller) => controller.User?.Identity?.GetUserId();


        public static UserProfile GetUserProfile(this Controller controller, IUserRepository userRepository)
        {
            string userId = controller.GetAppUserId();
            return string.IsNullOrEmpty(userId) ? null : userRepository.GetUserProfile(userId);
        }

        public static string BadRequestAndCollectEx(this Controller controller, Exception ex)
        {
            controller.Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;

            return ex.CollectMessages();
        }
    }
}