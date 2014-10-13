using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(html2xml.Startup))]
namespace html2xml
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
