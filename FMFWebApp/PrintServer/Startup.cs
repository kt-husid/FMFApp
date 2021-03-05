using Owin;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PrintServer
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.

        /// <summary>
        /// http://www.asp.net/web-api/overview/security/enabling-cross-origin-requests-in-web-api
        /// </summary>
        /// <param name="appBuilder"></param>
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();

            config.EnableCors();
            
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { action = "Get", id = RouteParameter.Optional }
            );

            appBuilder.UseWebApi(config);
        }
    }
}