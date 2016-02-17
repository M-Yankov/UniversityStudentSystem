namespace UniversityStudentSystem.Web.Areas.Admin
{
    using System.Web.Mvc;
    using Constraints;

    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Trainer";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Trainer",
                "Trainer/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new  { isAllowed = new TrainerRouteConstraint() }
            );
        }
    }
}