﻿namespace UniversityStudentSystem.RouteTests
{
    using System.Web.Mvc;
    using System.Web.Routing;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MvcRouteTester;
    using UniversityStudentSystem.Web.Areas.Public;
    using UniversityStudentSystem.Web.Areas.Public.Controllers;
    using Web;

    [TestClass]
    public class PublicAreaCourseRouteTests
    {
        private RouteCollection routeCollection;

        [TestInitialize]
        public void Initialize()
        {
            var areaRegistration = new PublicAreaRegistration();
            this.routeCollection = new RouteCollection();

            var areaRegistrationContext = new AreaRegistrationContext(areaRegistration.AreaName, this.routeCollection);
            areaRegistration.RegisterArea(areaRegistrationContext);
            RouteConfig.RegisterRoutes(this.routeCollection);
        }

        [TestMethod]
        public void ShouldMapCourseDetailsWithOprionalParametersIdAndName()
        {
            const int CourseId = 2;

            this.routeCollection
                .ShouldMap($"/Public/Courses/Details/{ CourseId }/My-NewCourse")
                .To<CoursesController>(c => c.Details(2));
        }

        [TestMethod]
        public void ShouldMapDefaultCourseActionWithGivenPageParameter()
        {
            const int Page = 3;
            string url = $"/Public/Courses/Index?page={ Page }";

            this.routeCollection.ShouldMap(url).To<CoursesController>(c => c.Index(Page));
        }

        [TestCleanup]
        public void Clean()
        {
            this.routeCollection = null;
        }
    }
}
