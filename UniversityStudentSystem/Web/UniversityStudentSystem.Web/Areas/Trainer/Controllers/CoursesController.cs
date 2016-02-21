namespace UniversityStudentSystem.Web.Areas.Trainer.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using Data.Models;
    using Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Models;
    using Services.Contracts;
    using UniversityStudentSystem.Web.Controllers;
    using Web.Models.Courses;
    using Web.Models.CoursesTask;
    using Web.Models.Marks;
    using Web.Models.Resources;
    using Web.Models.Solutions;
    using Web.Models.Users;
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

        public ActionResult Solutions(int id)
        {
            var course = this.courseService.GetAll().FirstOrDefault(c => c.Id == id);
            var solutions = course.Solutions
                .AsQueryable()
                .Distinct(new SolutionEqialityComparer())
                .To<SolutionViewModel>();

            return this.View(solutions);
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

            return this.RedirectToAction("Details", "Courses", new { id = id, area = "Public" });
        }

        public ActionResult AddTask(int id)
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTask(TaskInputModel model, int id)
        {
            model.Requirements = HttpUtility.HtmlDecode(model.Requirements);
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

        public ActionResult Edit(int id)
        {
            var courseForEdit = this.courseService.GetAll().FirstOrDefault(c => c.Id == id);
            if (courseForEdit == null)
            {
                return this.RedirectToAction("NotFound");
            }

            var modelForEdit = this.Mapper.Map<CourseInputModel>(courseForEdit);

            return this.View(modelForEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CourseInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var modelFromDb = this.courseService.GetAll().FirstOrDefault(c => c.Id == id);
            if (modelFromDb == null)
            {
                return this.RedirectToAction("NotFound");
            }

            this.Mapper.Map(model, modelFromDb);
            this.courseService.Edit(modelFromDb);

            return this.RedirectToAction("Details", "Courses", new { area = "Public", id = id });
        }
        
        public ActionResult Upload(int id)
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(int id,  ResourceInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            string pathDb = "/Resources/" + model.File.FileName.Replace(" ", "-");
            string pathFileSystem = this.Server.MapPath("~/Resources/" + model.File.FileName.Replace(" ", "-"));

            model.File.SaveAs(pathFileSystem);
            this.courseService.AddResourse(model.Name, pathDb, id);
           
            return this.RedirectToAction("Details", "Courses", new { area = "Public", id = id });
        }

        public ActionResult Marks(int id)
        {
            return this.View();
        }

        public ActionResult GetMarks(int id, [DataSourceRequest] DataSourceRequest request)
        {
            var course = this.courseService.GetAll().FirstOrDefault(c => c.Id == id);
            if (course == null)
            {
                return this.RedirectToAction("NotFound");
            }

            var viewModel = course.Marks.AsQueryable().To<MarkInputModel>().ToList();

            return this.Json(viewModel.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddMark([DataSourceRequest] DataSourceRequest request, int id, MarkInputModel model)
        {
            // No idea why id is 0 when path is /Trainer/Courses/AddMark/5;
            int courseId = int.Parse(this.Request.RequestContext.RouteData.Values["id"].ToString());
            var returnResult = this.RedirectToAction("GetMarks", new {  id = id});

            if (!this.ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Bad Request");
            }

            this.courseService.AddMark(model.Value, model.Username, courseId, model.Reason);
            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState));
        }

        public ActionResult GetUsernames(int id, [DataSourceRequest] DataSourceRequest request, string text)
        {
            // TODO: how to cache this ?
            var usernames = this.courseService
                .GetAll()
                .FirstOrDefault(c => c.Id == id)
                .Semester
                .Specialty
                .Students
                .Where(u => u.UserName.StartsWith(text))
                .Select(u => u.UserName)
                .ToArray();
             
            return this.Json(usernames, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GiveMark(int id)
        {
            return this.View();
        }
    }
}