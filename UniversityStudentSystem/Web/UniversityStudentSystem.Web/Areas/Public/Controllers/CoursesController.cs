namespace UniversityStudentSystem.Web.Areas.Public.Controllers
{
    using System.Web.Mvc;
    using Services.Contracts;
    using Web.Controllers;
    using System.Linq;
    using UniversityStudentSystem.Web.Infrastructure.Mapping;
    using Models.Courses;
    using Common;
    using Data.Models;
    public class CoursesController : BaseController
    {
        private ICoursesService courseService;

        public CoursesController(ICoursesService service)
        {
            this.courseService = service;
        }

        // GET: Public/Courses
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
    }
}