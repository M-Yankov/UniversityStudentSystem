namespace UniversityStudentSystem.Web.Areas.Admin
{
    using System.Web.Mvc;
    using UniversityStudentSystem.Web.Constraints;

    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}/{name}",
                new { action = "Index", id = UrlParameter.Optional, name = UrlParameter.Optional },
                new { isAllowed = new AdminRouteContraint() });
        }
    }
}