namespace UniversityStudentSystem.Web.Areas.Public.Controllers
{
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Common;
    using Data.Models;
    using HelperProviders;
    using Models.Courses;
    using Services.Contracts;
    using UniversityStudentSystem.Web.Infrastructure.Mapping;
    using Web.Controllers;
    using Web.Models.Tests;

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

            string reasonWhenIsNotAllowed = this.courseService.IsAllowed(this.UserId, id);
            this.ViewBag.Path = this.courseService.SolutionResult(this.UserId, id);

            if (reasonWhenIsNotAllowed == null)
            {
                this.ViewBag.IsAllowed = true;
            }
            else
            {
                this.ViewBag.IsAllowed = false;
                this.ViewBag.Message = reasonWhenIsNotAllowed;
            }

            var viewModel = this.Mapper.Map<CourseViewModel>(course);
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult UploadSolution(int id, HttpPostedFileBase file)
        {
            ActionResult redirectResult = this.RedirectToAction(
                "Details",
                "Courses",
                new { area = "Public", id = id });

            UploadResult result = new UploadResult();
            if (file == null)
            {
                return redirectResult;
            }

            result = this.UserManagement.SaveSolution(file, this.UserId, id);
            if (!result.HasSucceed)
            {
                this.TempData["Error"] = result.Error;
            }
            else
            {
                this.courseService.SaveSolution(result.Path, this.UserId, id);
            }

            return redirectResult;
        }

        [Authorize]
        public ActionResult MakeTest(int id)
        {
            var test = this.courseService.GetTestForStudent(id, this.UserId);
            if (test == null)
            {
                return this.View("NoTests");
            }

            var testModel = this.Mapper.Map<TestForStudentModel>(test);
            return this.View(testModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult MakeTest(int id, TestInputModel model)
        {
            var result = this.RedirectToAction("MakeTest", new { id = id });
            if (!this.ModelState.IsValid)
            {
                this.TempData["Message"] = "Please check test form again!";
                return result;
            }

            var test = this.courseService.GetAll()
                .FirstOrDefault(c => c.Id == id)
                .Tests
                .FirstOrDefault(t => t.Id == model.TestId);

            if (test.Questions.Count != model.Questions.Count)
            {
                this.TempData["Message"] = "Fill all questions";
                return result;
            }

            var testResult = this.courseService.SolveTest(id, this.UserId, model.TestId, model.Questions);
            return this.RedirectToAction("TestResult", "Courses", new { id = testResult.Id, area = "Public" });
        }

        public ActionResult TestResult(int id)
        {
            var result = this.courseService.GetResult(id);
            var viewModel = this.Mapper.Map<TestResultViewModel>(result);
            return this.View(viewModel);
        }
    }
}