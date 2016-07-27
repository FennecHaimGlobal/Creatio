using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CreatioFrance.Startup))]
namespace CreatioFrance
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
