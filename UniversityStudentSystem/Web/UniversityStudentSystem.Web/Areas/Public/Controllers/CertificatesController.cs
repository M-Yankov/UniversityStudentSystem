namespace UniversityStudentSystem.Web.Areas.Public.Controllers
{
    using System.Web.Mvc;
    using UniversityStudentSystem.Web.Controllers;

    public class CertificatesController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}