
namespace UniversityStudentSystem.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Models;
    using UniversityStudentSystem.Web.Controllers;

    public class CoursesController : BaseController
    {
        // GET: Admin/Courses
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddTest()
        {
            TestInputModel model = new TestInputModel()
            {
                Name = "Test example",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMinutes(30),
                IsEnabled = false,
                Questions = new List<QuestionInputModel>()
                {
                    new QuestionInputModel()
                    {
                        Points = 20,
                        Content = "Content of question 1",
                        Answers = new List<AnswerInputModel>()
                        {
                            new AnswerInputModel() {Content = "Answer 1" },
                            new AnswerInputModel() {Content = "Answer 2" },
                            new AnswerInputModel() {Content = "Answer 3" }
                        }
                    },
                    new QuestionInputModel()
                    {
                        Points = 15,
                        Content = "Content of question 2",
                        Answers = new List<AnswerInputModel>()
                        {
                            new AnswerInputModel() {Content = "Answer 1 forQuestion 2" },
                            new AnswerInputModel() {Content = "Answer 2 forQuestion 2" },
                            new AnswerInputModel() {Content = "Answer 3 forQuestion 2" }
                        }
                    },

                    new QuestionInputModel()
                    {
                        Points = 10,
                        Content = "Content of question 3",
                        Answers = new List<AnswerInputModel>()
                        {
                            new AnswerInputModel() {Content = "Third 1" },
                            new AnswerInputModel() {Content = "Third 2" },
                            new AnswerInputModel() {Content = "Third 3" }
                        }
                    },
                }
            };

            // bool? to bool cannot be converted in the template .cshtml :| :(
            return this.View();
        }

        [HttpPost]
        public ActionResult AddTest(TestInputModel model)
        {
            return this.View(model);
        }
    }
}