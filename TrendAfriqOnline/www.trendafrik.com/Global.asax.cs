using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace TrendAfriqOnline
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        private static DateTime whenTaskLastRan;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            System.Data.Entity.Database.SetInitializer<TrendAfriqOnline.Models.TrendAfriqEntities>(null);

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            whenTaskLastRan = DateTime.Now;

            BundleMobileConfig.RegisterBundles(BundleTable.Bundles);
        }

        void Session_Start(object sender, EventArgs e)
        {
            //send emails to sellers that their expiry dates is in 7 days
            SendEmailsToSellersForExpiryRenewal();
            //send email to Admin / Sellers that advert expiry is due
            SendEmailForAdvertExpiry();
        }

        static void SendEmailsToSellersForExpiryRenewal()
        {
            DateTime oneDayAgo = DateTime.Now;
            if (whenTaskLastRan.CompareTo(oneDayAgo) <= 0)//(whenTaskLastRan.IsOlderThan(oneDayAgo))
            {
                // now run code to send email and reset our whentasklastrancode.;
                TABusinessLayer.SendEmails sd = new TABusinessLayer.SendEmails();
                sd.CheckExpiredRegistration();

                whenTaskLastRan = DateTime.Now;
            }
        }

        static void SendEmailForAdvertExpiry()
        {
            DateTime oneDayAgo = DateTime.Now;
            if (whenTaskLastRan.CompareTo(oneDayAgo) <= 0)//(whenTaskLastRan.IsOlderThan(oneDayAgo))
            {
                // now run code to send email and reset our whentasklastrancode.;
                TABusinessLayer.SendEmails sd = new TABusinessLayer.SendEmails();
                sd.CheckExpiredAdvertSlots();

                whenTaskLastRan = DateTime.Now;
            }
        }

    }
}