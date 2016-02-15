namespace UniversityStudentSystem.Web.Areas.Public
{
    using System.Web.Mvc;

    public class PublicAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Public";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            // TODO: default Controller
            context.MapRoute(
                "Public_default",
                "Public/{controller}/{action}/{id}/{name}",
                new { action = "Index", id = UrlParameter.Optional, name = UrlParameter.Optional }
            );
        }
    }
}