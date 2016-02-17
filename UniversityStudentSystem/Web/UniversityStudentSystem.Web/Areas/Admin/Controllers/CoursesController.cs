namespace UniversityStudentSystem.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Data.Models;
    using Models;
    using Services.Contracts;
    using UniversityStudentSystem.Web.Controllers;

    public class CoursesController : BaseController
    {
        private ITestService testService;

        public CoursesController(ITestService testService)
        {
            this.testService = testService;
        }

        // GET: Admin/Courses
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
    }
}