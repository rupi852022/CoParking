using ParkingProject.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Threading;
using ParkingProject.Models;
using ParkingProject.Models.DAL;

namespace ParkingProject
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            DataServicesConfig.CreateTables();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Thread thr = new Thread(ThreadProc);
            thr.Start();
        }

        public static void ThreadProc()
        {
            DataServices ds = new DataServices();
            while (1!=0)
            {
                ds.updatePriorityUsers();
                Thread.Sleep(43200000);//הפעלה כל 12 שעות
            }
        }
    }
}
