using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Data.Entity;
namespace Flashminder
{

    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            //RouteTable.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = System.Web.Http.RouteParameter.Optional }
            //);
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
            RouteTable.Routes.MapHttpRoute(
                name:"Algorithm",
                routeTemplate:"api/Supermemo/{id}/{multiplier}",
                defaults: new { controller="Supermemo", id = "", multiplier = "1"},
                constraints: new {id = @"\d+"}
                );

            RouteTable.Routes.MapHttpRoute(
                name:"Database",
                routeTemplate:"api/db/{modifiedDate}",
                defaults: new { controller = "Database", modifiedDate = System.Web.Http.RouteParameter.Optional }
            );

            RouteTable.Routes.MapHttpRoute(
            name: "Flashcard",
            routeTemplate: "api/flashcard/{id}",
            defaults: new { controller = "Flashcard" }
            );
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}