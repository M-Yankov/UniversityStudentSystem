namespace UniversityStudentSystem.Web.Areas.Public.Controllers
{
    using System.Web.Mvc;
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

        // GET: Public/Report
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(BugReportInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            this.reportService.Create(model.Content, model.Email);
            return this.RedirectToAction("Index", "Home", new { area = string.Empty });
        }
    }
}