namespace UniversityStudentSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using UniversityStudentSystem.Services.Contracts;

    public class HomeController : Controller
    {
        private IMyService servc;

        public HomeController(IMyService servc)
        {
            this.servc = servc;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}