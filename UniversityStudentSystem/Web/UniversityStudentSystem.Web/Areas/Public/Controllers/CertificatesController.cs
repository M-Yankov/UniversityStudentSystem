namespace UniversityStudentSystem.Web.Areas.Public.Controllers
{
    using System.Web.Mvc;
    using Services.Contracts;
    using UniversityStudentSystem.Web.Controllers;
    using UniversityStudentSystem.Common;
    using Models;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System;
    using Models.Certificates;
    using System.Linq;
    public class CertificatesController : BaseController
    {
        private ICertificateService certificatesService;
        private ISpecialtiesService specialtiesService;

        public CertificatesController(
            ICertificateService certificatesService, 
            ISpecialtiesService specialtiesService)
        {
            this.certificatesService = certificatesService;
            this.specialtiesService = specialtiesService;
        }

        public ActionResult Index()
        {
            var imageContent = this.certificatesService.MakeCertificate(
                this.Server.MapPath(WebConstants.PathToCertificate), 
                "Your names will be here",
                "Specialty name", 
                DateTime.Now,
                DateTime.Now.AddYears(1));
            
            return View(new ImageCertificate() { Data = imageContent });
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Admin + ", " + RoleConstants.Trainer)]
        [ValidateAntiForgeryToken]
        public ActionResult GiveCertificate(CertificateInputModel model)
        {
            this.UserManagement.EnsureFolder(model.UserId);
            string path = this.UserManagement.GetCurrentUserDirecotry(model.UserId);

            this.certificatesService.GiveToPerson(
                model.UserId,
                model.SpecialtyId,
                System.IO.Path.Combine(path, "Uploads"),
                Server.MapPath(WebConstants.PathToCertificate));

            return this.RedirectToAction(
                "Students", 
                "Specialties",
                new { id = model.SpecialtyId, area = "Trainer" });
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Admin + ", " + RoleConstants.Trainer)]
        [ValidateAntiForgeryToken]
        public ActionResult GiveCertificateToAll(CertificateInputModel model)
        {
            var specialty = this.specialtiesService.GetAll().FirstOrDefault(s => s.Id == model.SpecialtyId);

            foreach (var student in specialty.Students)
            {
                this.UserManagement.EnsureFolder(student.Id);
                string path = this.UserManagement.GetCurrentUserDirecotry(student.Id);

                this.certificatesService.GiveToPerson(
                    student.Id,
                    specialty.Id,
                    System.IO.Path.Combine(path, "Uploads"),
                    Server.MapPath(WebConstants.PathToCertificate));
            }

            return this.RedirectToAction(
                "Students", 
                "Specialties", 
                new { id = model.SpecialtyId, area = "Trainer" });
        }
    }
}