using cshh.Model.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace cshh.Asp.Models.Common
{
    public class WorkContext : IWorkContext
    {
        public WorkContext()
        {

        }
        public string UserAppId
        {
            get
            {
                return HttpContext.Current.User?.Identity?.GetUserId();
            }
        }

        //public ClaimsIdentity ClaimsIdentity => ((ClaimsIdentity)HttpContext.Current.User?.Identity);
    }
}