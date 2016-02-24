namespace UniversityStudentSystem.RouteTests
{
    using System.Web.Mvc;
    using System.Web.Routing;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MvcRouteTester;
    using UniversityStudentSystem.Web.Areas.Public;
    using UniversityStudentSystem.Web.Areas.Public.Controllers;
    using Web;
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

            var areaRegistrationContext = new AreaRegistrationContext(areaRegistration.AreaName, this.routeCollection);
            areaRegistration.RegisterArea(areaRegistrationContext);
            RouteConfig.RegisterRoutes(this.routeCollection);
        }

        [TestMethod]
        public void ShouldMapToDefaultNewsPage()
        {
            const string Url = "/Public/News/";
            this.routeCollection.ShouldMap(Url).To<NewsController>(c => c.Index());
        }

        [TestMethod]
        public void ShouldMapToDefaultNewsPageWithGivenAction()
        {
            const string Url = "/Public/News/Index";
            this.routeCollection.ShouldMap(Url).To<NewsController>(c => c.Index());
        }

        [TestMethod]
        public void ShouldMapToNewsDetailsPageWithGivenId()
        {
            const int Id = 5;
            string url = "/Public/News/Details/" + Id.ToString();
            this.routeCollection.ShouldMap(url).To<NewsController>(c => c.Details(Id));
        }

        [TestMethod]
        public void ShouldMapToNewsDetailsPageWithGivenNameAndId()
        {
            const int Id = 5;
            const string NewsName = "Welcome-to-the-University";
            string url = $"/Public/News/Details/{ Id.ToString() }/{ NewsName }";
            this.routeCollection.ShouldMap(url).To<NewsController>(c => c.Details(Id));
        }

        [TestMethod]
        public void ShouldMapToNonExistingRoute()
        {
            const int Id = 5;
            const string NewsName = "Welcome-to-the-University";
            string url = $"/Public/News/Details/{ Id.ToString() }/{ NewsName }/NonExist";
            this.routeCollection.ShouldMap(url).ToNoRoute();
        }

        [TestMethod]
        public void ShouldMapCreateNewsRouteWithCorrentData()
        {
            string newsContent = "News Content";
            string newsTitle = "News Title";
            var model = new NewsInputModel() { Content = newsContent, Title = newsTitle };

            this.routeCollection.ShouldMap("/Public/News/Create")
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
