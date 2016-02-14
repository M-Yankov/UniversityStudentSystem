namespace UniversityStudentSystem.Web.Controllers
{
    using System.Web;
    using System.Web.Mvc;

    public class ErrorController : BaseController
    {
        public ActionResult NotFound()
        {
            return this.View();
        }
    }
}