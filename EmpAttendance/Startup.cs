using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EmpAttendance.Startup))]
namespace EmpAttendance
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
