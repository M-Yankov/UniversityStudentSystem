namespace UniversityStudentSystem.Web.Areas.Trainer
{
    using System.Web.Mvc;
    using Constraints;

    public class TrainerAreaRegistration : AreaRegistration
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
                "Trainer/{controller}/{action}/{id}/{name}",
                new { action = "Index", id = UrlParameter.Optional, name = UrlParameter.Optional },
                new { isAllowed = new TrainerRouteConstraint() });
        }
    }
}