namespace UniversityStudentSystem.Web.Controllers
{
    using System.Web;
    using System.Web.Mvc;
    using UniversityStudentSystem.Common;

    [Authorize]
    public class ChatController : BaseController
    {
        public ActionResult Index()
        {
            this.Response.Cookies.Add(new HttpCookie(WebConstants.CookieKeyForChar, this.User.Identity.Name));
            return this.View();
        }
    }
}