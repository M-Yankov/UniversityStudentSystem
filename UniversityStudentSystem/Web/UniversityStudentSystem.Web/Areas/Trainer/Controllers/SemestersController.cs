namespace UniversityStudentSystem.Web.Areas.Trainer.Controllers
{
    using System.Web.Mvc;
    using Services.Contracts;
    using UniversityStudentSystem.Web.Controllers;

    public class SemestersController : BaseController
    {
        private ISemesterService semesterService;

        public SemestersController(ISemesterService semesterService)
        {
            this.semesterService = semesterService;
        }

        public ActionResult Activate(int id)
        {
            int specialtyId = this.semesterService.ChangeStatus(id);
            return this.RedirectToAction("Details", "Specialties", new { id = specialtyId, area = "Public" });
        }

        public ActionResult Deactivate(int id)
        {
            int specialtyId = this.semesterService.ChangeStatus(id);
            return this.RedirectToAction("Details", "Specialties", new { id = specialtyId, area = "Public" });
        }
    }
}