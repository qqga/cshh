using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
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

            if(!HostingEnvironment.IsDevelopmentEnvironment)
            {
                EncryptSections("connectionStrings", "appSettingsEncrypted");
            }
            

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //[Conditional("ENCRYPT")]
        public void EncryptSections(params string[] sectionsNames)
        {
           
            Configuration config = WebConfigurationManager.OpenWebConfiguration("/"); // try it if some issue "/" or HttpContext.Current.Request.ApplicationPath

            foreach(string sectionName in sectionsNames)
            {
                ConfigurationSection section = config.GetSection(sectionName);
                if(!section.SectionInformation.IsProtected)
                {
                    section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");//RsaProtectedConfigurationProvider
                    config.Save();
                }
            }
        }      
    }

}
