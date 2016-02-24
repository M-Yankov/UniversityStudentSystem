namespace UniversityStudentSystem.ControllerTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Data.Models;
    using Kendo.Mvc.UI;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Services.Contracts;
    using TestStack.FluentMVCTesting;
    using UniversityStudentSystem.Web.Areas.Trainer.Controllers;
    using Web.Infrastructure.Mapping;
    using Web.Models.Courses;
    using Web.Models.Marks;
    using Web.Models.Resources;

    [TestClass]
    public class TrainerAreaCoursesControllerTests
    {
        [TestMethod]
        public void ExpectAddMarkToReturnBadRequestWhenModelStateIsInvalid()
        {
            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(typeof(CoursesController).Assembly);
            const string ResultContent = "Bad Request";

            var courseServiceMock = new Mock<ICoursesService>();
            var contextMock = new Mock<HttpContextBase>();

            var routeData = new RouteData();
            routeData.Values.Add("id", 6);

            contextMock.SetupGet(x => x.Request.RequestContext)
                .Returns(new RequestContext(contextMock.Object, routeData));

            courseServiceMock.Setup(x =>
                x.AddMark(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()));

            var controller = new CoursesController(null, courseServiceMock.Object);

            //// Set ModelState to false
            controller.ModelState.AddModelError("Property", "Cannot be null");
            controller.ControllerContext = new ControllerContext(contextMock.Object, routeData, controller);
            var result = controller
                .WithCallTo(x => x.AddMark(null, 2, new MarkInputModel() { }))
                .ShouldGiveHttpStatus(HttpStatusCode.BadRequest);

            Assert.AreEqual(ResultContent, result.StatusDescription);
        }

        [TestMethod]
        public void ExpectAddMarkToAddMarkWithoutErrors()
        {
            var courseServiceMock = new Mock<ICoursesService>();
            var contextMock = new Mock<HttpContextBase>();
            const int CourseId = 6;

            const string Username = "john.Kellin";
            const int Value = 4;

            var routeData = new RouteData();
            routeData.Values.Add("id", CourseId);

            contextMock
                .SetupGet(x => x.Request.RequestContext)
                .Returns(new RequestContext(contextMock.Object, routeData));

            courseServiceMock.Setup(x =>
                    x.AddMark(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()))
                    .Callback<int, string, int, string>((markValue, userName, courseId, reason) =>
                        {
                            Assert.AreEqual(CourseId, courseId);
                            Assert.AreEqual(Username, userName);
                            Assert.AreEqual(Value, markValue);
                            Assert.IsNull(reason);
                        });

            var controller = new CoursesController(null, courseServiceMock.Object);
            var markModel = new MarkInputModel()
            {
                Value = Value,
                Username = Username
            };

            controller.ControllerContext = new ControllerContext(contextMock.Object, routeData, controller);
            controller.WithCallTo(x => x.AddMark(new DataSourceRequest(), 2, markModel))
                .ShouldReturnJson();
        }

        [TestMethod]
        public void ExpectTrainerToUploadResourseFileSuccessfuly()
        {
            const string ResourceName = "Testing Resource";
            const string FullFileName = "Test File.pdf";
            const int CourseId = 3;

            string expectedDbPath = "/Resources/" + FullFileName.Replace(" ", "-");
            string expectedServerPath = "C:/Resources/" + FullFileName.Replace(" ", "-");

            var courseServiceMock = new Mock<ICoursesService>();
            var fileMock = new Mock<HttpPostedFileBase>();
            var serverMock = new Mock<HttpServerUtilityBase>();
            var httpContextMock = new Mock<HttpContextBase>();

            serverMock.Setup(s => s.MapPath(It.IsAny<string>())).Returns<string>((c) =>
            {
                return "C:/" + c.Replace("~/", string.Empty);
            });

            httpContextMock.Setup(h => h.Server).Returns(serverMock.Object);

            courseServiceMock.Setup(c => c.AddResourse(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                .Callback<string, string, int>((name, path, courseId) =>
                {
                    Assert.AreEqual(expectedDbPath, path);
                    Assert.AreEqual(ResourceName, name);
                    Assert.AreEqual(CourseId, courseId);
                });

            fileMock.Setup(f => f.FileName).Returns(FullFileName);

            fileMock.Setup(f => f.SaveAs(It.IsAny<string>()))
                .Callback<string>(serverPath =>
                {
                    Assert.AreEqual(expectedServerPath, serverPath);
                });

            var controller = new CoursesController(null, courseServiceMock.Object);
            var resourceModel = new ResourceInputModel()
            {
                File = fileMock.Object,
                Name = ResourceName
            };

            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);

            controller.WithCallTo(c => c.Upload(CourseId, resourceModel))
                .ShouldRedirectTo<Web.Areas.Public.Controllers.CoursesController>(c => c.Details(CourseId));
        }

        [TestMethod]
        public void ExpectToReturnDefaultViewForUploadResourse()
        {
            const int CourseId = 2;
            var controller = new CoursesController(null, null);

            controller.WithCallTo(c => c.Upload(CourseId))
                .ShouldRenderView("Upload");
        }

        [TestMethod]
        public void ExpectToEditTestWithNoProblems()
        {
            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(typeof(CoursesController).Assembly);

            var courseServiceMock = new Mock<ICoursesService>();
            
            const string ExpectedResult = "Changed";
            const int CourseId = 3;

            courseServiceMock.Setup(c => c.GetAll()).Returns(new List<Course>
            {
                new Course()
                {
                    Id = CourseId,
                    Description = "Test course description",
                    Name = "Test Course",
                }
            }.AsQueryable());

            courseServiceMock.Setup(c => c.Edit(It.IsAny<Course>()))
                .Callback<Course>((course) =>
                {
                    Assert.AreEqual(ExpectedResult, course.Description);
                    Assert.AreEqual(ExpectedResult, course.Name);
                });

            var coursesController = new CoursesController(null, courseServiceMock.Object);
            var inputModel = new CourseInputModel()
            {
                Description = ExpectedResult,
                Name = ExpectedResult
            };

            coursesController.WithCallTo(c => c.Edit(CourseId, inputModel))
                .ShouldRedirectTo<Web.Areas.Public.Controllers.CoursesController>(c => c.Details(CourseId));
        }

        [TestMethod]
        public void ExpectToRedirectToNotFoundActionWhenCourseIsNull()
        {
            var courseServiceMock = new Mock<ICoursesService>();

            const int CourseId = 3;

            courseServiceMock.Setup(c => c.GetAll()).Returns(new List<Course>
            {
                new Course()
                {
                    Id = CourseId,
                    Description = "Test course description",
                    Name = "Test Course",
                }
            }.AsQueryable());

            var coursesController = new CoursesController(null, courseServiceMock.Object);

            coursesController.WithCallTo(c => c.Edit(8, null))
                .ValidateActionReturnType<RedirectToRouteResult>();
        }

        [TestMethod]
        public void ExpectToRenderEditView()
        {
            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(typeof(CoursesController).Assembly);
            const int CourseId = 3;

            var courseServiceMock = new Mock<ICoursesService>();
            courseServiceMock.Setup(c => c.GetAll()).Returns(new List<Course>
            {
                new Course()
                {
                    Id = CourseId,
                    Description = "Test course description",
                    Name = "Test Course",
                }
            }.AsQueryable());

            var coursesController = new CoursesController(null, courseServiceMock.Object);

            coursesController.WithCallTo(c => c.Edit(CourseId))
                .ShouldRenderDefaultView()
                .WithModel<CourseInputModel>()
                .AndNoModelErrors();
        }

        [TestMethod]
        public void ExpectToRenderJoinForm()
        {
            const int CourseId = 3;
            var courseServiceMock = new Mock<ICoursesService>();
            courseServiceMock.Setup(c => c.GetAll()).Returns(new List<Course>
            {
                new Course()
                {
                    Id = CourseId,
                    Description = "Test course description",
                    Name = "Test Course",
                }
            }.AsQueryable());

            var coursesController = new CoursesController(null, courseServiceMock.Object);

            coursesController.WithCallTo(c => c.Join(CourseId))
                .ShouldRenderDefaultView();
        }
    }
}
