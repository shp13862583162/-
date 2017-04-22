using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LibraryManageMVC.Startup))]
namespace LibraryManageMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
