namespace UniversityStudentSystem.Web.Areas.Trainer.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Data.Models;
    using Models;
    using Services.Contracts;
    using UniversityStudentSystem.Web.Controllers;
    using Web.Models.CoursesTask;

    public class CoursesController : BaseController
    {
        private ITestService testService;
        private ICoursesService courseService;

        public CoursesController(ITestService testService, ICoursesService courseService)
        {
            this.testService = testService;
            this.courseService = courseService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddTest(int id)
        {
            return this.View();
        }

        //// id = courseId
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTest(int id, TestInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            // Maaaagic :)
            var mapp = this.Mapper.Map<Test>(model);
            this.testService.Create(mapp, id);

            return this.View(model);
        }

        public ActionResult AddTask(int id)
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTask(TaskInputModel model, int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var dbCourseTask = this.Mapper.Map<CourseTask>(model);
            this.courseService.AddTask(dbCourseTask, id);

            return this.RedirectToAction("Details", "Courses", new { id = id, area = "Public" });
        }

        public ActionResult Join(int id)
        {
            var course = this.courseService.GetAll().FirstOrDefault(c => c.Id == id);
            if (course == null)
            {
                return this.RedirectToAction("NotFound");
            }

            this.ViewBag.CourseName = course.Name;
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult JoinIn(int id)
        {
            this.courseService.JoinIn(id, this.UserId);
            return this.RedirectToAction("Details", "Courses", new { area = "Public", id = id });
        }
    }
}