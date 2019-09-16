using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FinancialPortals.Startup))]
namespace FinancialPortals
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
