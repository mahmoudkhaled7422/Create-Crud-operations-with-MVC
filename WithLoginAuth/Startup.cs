using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WithLoginAuth.Startup))]
namespace WithLoginAuth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
