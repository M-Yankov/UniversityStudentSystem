namespace UniversityStudentSystem.Web.Areas.Trainer.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Infrastructure.Mapping;
    using Services.Contracts;
    using Web.Models.Courses;
    using Web.Models.Semesters;
    using Web.Models.Users;

    public class SpecialtiesController : Controller
    {
        private ISpecialtiesService specialtiesService;
        private ICoursesService coursesService;

        public SpecialtiesController(ISpecialtiesService specialtiesService, ICoursesService coursesService)
        {
            this.specialtiesService = specialtiesService;
            this.coursesService = coursesService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult AddCourse(int id)
        {
            var specialty = this.specialtiesService.GetAll().FirstOrDefault(s => s.Id == id);
            if (specialty == null)
            {
                return this.RedirectToAction("NotFound");
            }

            var model = new CourseInputModel()
            {
                SpecialtyName = specialty.Name,
                Semesters = specialty.Semesters.AsQueryable().To<SemesterViewModel>().ToList(),
            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCourse(int id, CourseInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            int courseId = this.coursesService.AddCourse(model.Name, model.Description, model.SemesterId);
            return this.RedirectToAction("Details", "Courses", new { area = "Public", id = courseId });
        }

        public ActionResult Students(int id)
        {
            var specialty = this.specialtiesService.GetAll().FirstOrDefault(s => s.Id == id);
            if (specialty == null)
            {
                return this.RedirectToAction("NotFound");
            }

            this.ViewBag.SpecialtyName = specialty.Name;
            var students = specialty.Students.AsQueryable().To<UserViewModel>().ToList();

            return this.View(students);
        }
    }
}