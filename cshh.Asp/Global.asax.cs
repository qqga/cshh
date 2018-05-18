using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace cshh.Asp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            EncryptConnectionString();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        [Conditional("ENCRYPT")]
        public void EncryptConnectionString()
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration("/"); // try it if some issue "/" or HttpContext.Current.Request.ApplicationPath
            ConfigurationSection section = config.GetSection("connectionStrings");
            if(!section.SectionInformation.IsProtected)
            {
                section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");//RsaProtectedConfigurationProvider
                config.Save();
            }
        }

    }

}
