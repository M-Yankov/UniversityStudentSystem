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
    using Web.Models.Certificates;
    using Web.Models;
    [TestClass]
    public class PublicAreaTests
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
        public void ShouldMapToAboutControllerInPublicArea()
        {
            const string Url = "/Public/About";
            routeCollection.ShouldMap(Url).To<AboutController>(c => c.Index());
        }

        [TestMethod]
        public void ShouldMapToDefaultControllerInPublicArea()
        {
            string url = "/Public/";
            routeCollection.ShouldMap(url).To<NewsController>(c => c.Index());
        }

        [TestMethod]
        public void ShouldMapGivingCertificateForUser()
        {
            const int SpecId = 3;
            const string UserId = "1DFA-126DAC-91E3F-AB918";
            var certificate = new CertificateInputModel() { SpecialtyId = SpecId, UserId = UserId };

            routeCollection
                .ShouldMap("/Public/Certificates/GiveCertificate")
                .WithFormUrlBody($"SpecialtyId={ SpecId }&userId={ UserId }")
                .To<CertificatesController>(c => c.GiveCertificate(certificate));
        }

        [TestMethod]
        public void ShouldMapBugReportRoute()
        {
            string url = "/Public/Report";

            routeCollection
                .ShouldMap(url)
                .To<ReportController>(c => c.Index());
        }

        [TestMethod]
        public void ShouldMapPostNewBugWithParameters()
        {
            string url = "/Public/Report";
            const string ContentOfTheBug = "This is the content of the bug!";
            const string EmailOfTheBug = "example@mail.com";

            var report = new BugReportInputModel()
            {
                Content = ContentOfTheBug,
                Email = EmailOfTheBug
            };

            routeCollection
                .ShouldMap(url)
                .WithFormUrlBody($"Content={ ContentOfTheBug }&Email={ EmailOfTheBug }")
                .To<ReportController>(c => c.Index(report));
        }

        [TestCleanup]
        public void Clean()
        {
            this.routeCollection = null;
        }
    }
}
