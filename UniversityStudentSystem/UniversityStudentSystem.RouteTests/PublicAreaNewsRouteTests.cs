using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace UniversityStudentSystem.RouteTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Web.Routing;
    using MvcRouteTester;

    using UniversityStudentSystem.Web.Areas.Public.Controllers;
    using UniversityStudentSystem.Web.Areas.Public;
    using Web;
    using System.Web.Mvc;
    using Web.Models.NewsModels;

    [TestClass]
    public class PublicAreaNewsRoutetest
    {
        private RouteCollection routeCollection;

        [TestInitialize]
        public void Initialize()
        {
            var areaRegistration = new PublicAreaRegistration();
            this.routeCollection = new RouteCollection();

            var areaRegistrationContext = new AreaRegistrationContext(areaRegistration.AreaName, routeCollection);
            areaRegistration.RegisterArea(areaRegistrationContext);
            RouteConfig.RegisterRoutes(routeCollection);
        }

        [TestMethod]
        public void ShouldMapToDefaultNewsPage()
        {
            const string Url = "/Public/News/";
            routeCollection.ShouldMap(Url).To<NewsController>(c => c.Index());
        }

        [TestMethod]
        public void ShouldMapToDefaultNewsPageWithGivenAction()
        {
            const string Url = "/Public/News/Index";
            routeCollection.ShouldMap(Url).To<NewsController>(c => c.Index());
        }

        [TestMethod]
        public void ShouldMapToNewsDetailsPageWithGivenId()
        {
            const int id = 5;
            string url = "/Public/News/Details/" + id.ToString();
            routeCollection.ShouldMap(url).To<NewsController>(c => c.Details(id));
        }

        [TestMethod]
        public void ShouldMapToNewsDetailsPageWithGivenNameAndId()
        {
            const int id = 5;
            const string newsName = "Welcome-to-the-University";
            string url = $"/Public/News/Details/{ id.ToString() }/{ newsName }";
            routeCollection.ShouldMap(url).To<NewsController>(c => c.Details(id));
        }

        [TestMethod]
        public void ShouldMapToNonExistingRoute()
        {
            const int id = 5;
            const string newsName = "Welcome-to-the-University";
            string url = $"/Public/News/Details/{ id.ToString() }/{ newsName }/NonExist";
            routeCollection.ShouldMap(url).ToNoRoute();
        }

        [TestMethod]
        public void ShouldMapCreateNewsRouteWithCorrentData()
        {
            string newsContent = "News Content";
            string newsTitle = "News Title";
            var model = new NewsInputModel() { Content = newsContent, Title = newsTitle };

            routeCollection.ShouldMap("/Public/News/Create")
                .WithFormUrlBody($"Content={ newsContent }&Title={ newsTitle }")
                .To<NewsController>(c => c.Create(model));
        }

        [TestCleanup]
        public void Clean()
        {
            this.routeCollection = null;
        }
    }
}
