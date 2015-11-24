using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lab1_BookStore.Startup))]
namespace Lab1_BookStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
