using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VimmelOrebro.Startup))]
namespace VimmelOrebro
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
