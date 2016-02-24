namespace UniversityStudentSystem.RouteTests
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MvcRouteTester;
    using UniversityStudentSystem.Web;
    using UniversityStudentSystem.Web.Areas.Public;
    using Web.Areas.Public.Controllers;
    using Web.Models.Comments;
    using Web.Models.ForumPosts;

    [TestClass]
    public class PublicAreaForumPostsTests
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
        public void ShouldMapToForumPostForm()
        {
            const string Url = "/Public/Forum/Post";
            this.routeCollection.ShouldMap(Url).To<ForumController>(c => c.Post());
        }

        [TestMethod]
        public void ShouldMapToPostANewForumPostWithCorrectData()
        {
            const string Url = "/Public/Forum/Post";
            const string ForumPostTitle = "This title is for testing purposes";
            const string ForumPostContent = "Test content";
            const int ForumCategoryId = 3;

            var model = new ForumInputModel()
            {
                Title = ForumPostTitle,
                CategoryId = ForumCategoryId,
                Content = ForumPostContent
            };

            this.routeCollection.ShouldMap(Url)
                .WithFormUrlBody(
                    $"Title={ ForumPostTitle }&Content={ ForumPostContent }&CategoryId={ ForumCategoryId }")
                .To<ForumController>(c => c.Post(model));
        }

        [TestMethod]
        public void ShoudMapPostNewCommentToForumPostWithCorrectData()
        {
            const int ForumPostId = 12;
            const string CommentContent = "Test comment for posing";
            var model = new CommentInputModel()
            {
                Content = CommentContent
            };

            this.routeCollection.ShouldMap($"/Public/Forum/AddComment/{ ForumPostId }")
                .WithFormUrlBody($"Content={ CommentContent }")
                .To<ForumController>(c => c.AddComment(ForumPostId, model));
        }

        [TestCleanup]
        public void Clean()
        {
            this.routeCollection = null;
        }
    }
}
