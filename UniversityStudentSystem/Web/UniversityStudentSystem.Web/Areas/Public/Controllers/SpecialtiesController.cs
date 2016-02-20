namespace UniversityStudentSystem.Web.Areas.Public.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Infrastructure.Mapping;
    using Models.Specialties;
    using Services.Contracts;
    using Web.Controllers;

    public class SpecialtiesController : BaseController
    {
        public ISpecialtiesService specialties;

        public SpecialtiesController(ISpecialtiesService specialtiesService)
        {
            this.specialties = specialtiesService;
        }

        public ActionResult Index()
        {
            IEnumerable<SpecialtyViewModel> specialties = this.specialties.GetAll().To<SpecialtyViewModel>().ToList();
            return View(specialties);
        }

        public ActionResult Details(int id)
        {
            var specialty = specialties.GetAll().FirstOrDefault(s => s.Id == id);
            if (specialty == null)
            {
                return this.RedirectToAction("NotFound");
            }

            var specialtyViewModel = this.Mapper.Map<SpecialtyViewModel>(specialty);
            return this.View(specialtyViewModel);
        }
    }
}