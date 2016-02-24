namespace UniversityStudentSystem.Web.Areas.Trainer.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Infrastructure.Mapping;
    using Services.Contracts;
    using UniversityStudentSystem.Web.Controllers;
    using Web.Models.Candidates;

    public class CandidatesController : BaseController
    {
        private ICandidateService candidateService;

        public CandidatesController(ICandidateService candidates)
        {
            this.candidateService = candidates;
        }

        public ActionResult Index()
        {
            var candidatures = this.candidateService.GetAll().To<CandidateViewModel>().ToList();
            return this.View(candidatures);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Download(int id)
        {
            var documentDetails = this.candidateService.GetDocument(id);
            if (documentDetails == null)
            {
                return this.RedirectToAction("NotFound");
            }

            var contents = this.candidateService.GetFileContents(Server.MapPath(documentDetails.Path));

            string fileName = documentDetails.Path.Substring(documentDetails.Path.LastIndexOf("/") + 1);
            return this.File(contents, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confirm(int id)
        {
            this.candidateService.Confirm(id);
            return this.RedirectToAction("Index");
        }

        public ActionResult Reject(int id)
        {
            this.candidateService.Reject(id);
            return this.RedirectToAction("Index");
        }
    }
}