namespace UniversityStudentSystem.Web.Areas.Public.Controllers
{
    using System.Web.Mvc;
    using Web.Controllers;

    public class NewsController : BaseController
    {
        // GET: Public/News
        public ActionResult Index()
        {
            return View();
        }
    }
}