namespace UniversityStudentSystem.Web.Areas.Public.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Common;
    using Data.Models;
    using Models.Courses;
    using Services.Contracts;
    using UniversityStudentSystem.Web.Infrastructure.Mapping;
    using Web.Controllers;

    public class CoursesController : BaseController
    {
        private ICoursesService courseService;

        public CoursesController(ICoursesService service)
        {
            this.courseService = service;
        }

        public ActionResult Index(int page = 1)
        {
            IQueryable<Course> courses = courseService.GetAll();
            if (page < 1)
            {
                page = 1;
            }

            this.ViewBag.Pages = courses.Count() / WebConstants.PageSizeCourse;

            var viewModel = courses
                .OrderByDescending(c => c.CreatedOn)
                .Skip((page - 1) * WebConstants.PageSizeCourse)
                .Take(WebConstants.PageSizeCourse)
                .To<CourseViewModel>()
                .ToList();

            return View(viewModel);
        }

        public ActionResult Details(int id)
        {
            var course = courseService.GetAll().FirstOrDefault(c => c.Id == id);

            if (course == null)
            {
                return this.RedirectToAction("NotFound");
            }

            var viewModel = this.Mapper.Map<CourseViewModel>(course);
            return this.View(viewModel);
        }
    }
}