using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyTask.Web.Startup))]
namespace MyTask.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           
        }
    }
}
