using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(cshh.Asp.Startup))]
namespace cshh.Asp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
