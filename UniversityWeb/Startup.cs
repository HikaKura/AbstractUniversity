using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UniversityWeb.Startup))]
namespace UniversityWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
