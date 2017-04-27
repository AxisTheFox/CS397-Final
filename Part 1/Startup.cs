using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Part_1.Startup))]
namespace Part_1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
