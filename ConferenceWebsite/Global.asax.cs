using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace ConferenceWebsite
{
    public class Global : HttpApplication
    {
        public enum ConferenceRole { AuthorOf, PCChair, PCMember, None };
        public static string sqlError = "";
        public static string loginPersonId;
        public static ConferenceRole loginRole;

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}