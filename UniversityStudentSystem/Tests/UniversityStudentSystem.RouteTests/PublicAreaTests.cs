namespace UniversityStudentSystem.RouteTests
{
    using System.Web.Mvc;
    using System.Web.Routing;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MvcRouteTester;
    using UniversityStudentSystem.Web.Areas.Public;
    using UniversityStudentSystem.Web.Areas.Public.Controllers;
    using Web;
    using Web.Models;
    using Web.Models.Certificates;

    [TestClass]
    public class PublicAreaTests
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
        public void ShouldMapToAboutControllerInPublicArea()
        {
            const string Url = "/Public/About";
            this.routeCollection.ShouldMap(Url).To<AboutController>(c => c.Index());
        }

        [TestMethod]
        public void ShouldMapToDefaultControllerInPublicArea()
        {
            string url = "/Public/";
            this.routeCollection.ShouldMap(url).To<NewsController>(c => c.Index());
        }

        [TestMethod]
        public void ShouldMapGivingCertificateForUser()
        {
            const int SpecId = 3;
            const string UserId = "1DFA-126DAC-91E3F-AB918";
            var certificate = new CertificateInputModel() { SpecialtyId = SpecId, UserId = UserId };

            this.routeCollection
                .ShouldMap("/Public/Certificates/GiveCertificate")
                .WithFormUrlBody($"SpecialtyId={ SpecId }&userId={ UserId }")
                .To<CertificatesController>(c => c.GiveCertificate(certificate));
        }

        [TestMethod]
        public void ShouldMapBugReportRoute()
        {
            string url = "/Public/Report";

            this.routeCollection
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

            this.routeCollection
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
