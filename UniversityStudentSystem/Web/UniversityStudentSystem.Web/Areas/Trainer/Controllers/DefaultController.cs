using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityStudentSystem.Web.Areas.Trainer.Controllers
{
    public class MaikaTiPytkataController : Controller
    {
        // GET: Trainer/Default
        public ActionResult Index()
        {
            return View("Candidates");
        }
    }
}