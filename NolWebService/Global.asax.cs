using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Tracing;
using System.Web.Optimization;
using System.Web.Routing;
using System.Xml.Serialization;
using NolWebService.Models;
using NolWebService.Filter;
using NolWebService.Infrastructure;

namespace NolWebService
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        protected void Application_Start()
        {
            var config = GlobalConfiguration.Configuration;
            //config.Filters.Add(new NotImplExceptionFilterAttribute()); 
            config.Services.Replace(typeof(ITraceWriter), new NLogger());
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly;
            var xml =  config.Formatters.XmlFormatter;
            xml.Indent = true;            
            RegisterRoutes(RouteTable.Routes);
        }
    }
}