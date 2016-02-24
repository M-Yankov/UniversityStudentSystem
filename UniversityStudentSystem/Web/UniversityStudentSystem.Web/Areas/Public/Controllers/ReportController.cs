namespace UniversityStudentSystem.Web.Areas.Public.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Common;
    using Infrastructure.Mapping;
    using Models;
    using Services.Contracts;
    using UniversityStudentSystem.Web.Controllers;

    public class ReportController : BaseController
    {
        private IBugReportService reportService;

        public ReportController(IBugReportService bugReportService)
        {
            this.reportService = bugReportService;
        }
        
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(BugReportInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.reportService.Create(model.Content, model.Email);
            return this.RedirectToAction("Index", "Home", new { area = string.Empty });
        }

        [Authorize(Roles = RoleConstants.Admin + ", " + RoleConstants.Trainer)]
        public ActionResult All()
        {
            var bugs = this.reportService.GetAll().To<BugViewModel>().ToList();
            return this.View(bugs);
        }

        [Authorize(Roles = RoleConstants.Admin + ", " + RoleConstants.Trainer)]
        public ActionResult Details(int id)
        {
            var bug = this.reportService.GetAll().FirstOrDefault(b => b.Id == id);
            var bugDetails = this.Mapper.Map<BugViewModel>(bug);

            return this.View(bugDetails);
        }
    }
}