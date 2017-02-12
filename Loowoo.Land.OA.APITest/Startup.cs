using Loowoo.Land.OA.API;
using Owin;
using System.Web.Http;

namespace Loowoo.Land.OA.APITest
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var configuration = new HttpConfiguration();
            WebApiConfig.Register(configuration);
            app.UseWebApi(configuration);
        }
    }
}
