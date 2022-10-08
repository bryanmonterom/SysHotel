using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SysHotelv1.Startup))]
namespace SysHotelv1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
