namespace UniversityStudentSystem.Web.Areas.Trainer.Controllers
{
    using System.Web.Mvc;
    using Services.Contracts;
    using UniversityStudentSystem.Web.Controllers;
    using System.Linq;
    using System;
    using Infrastructure.Mapping;
    using Web.Models.Candidates;
    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
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
            return File(contents, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confirm(int id)
        {
            return this.RedirectToAction("Index");
        }

        public ActionResult Reject(int id)
        {
            return this.RedirectToAction("Index");
        }
    }
}