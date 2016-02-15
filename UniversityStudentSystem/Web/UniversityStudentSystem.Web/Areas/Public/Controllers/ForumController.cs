namespace UniversityStudentSystem.Web.Areas.Public.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Models.Comments;
    using Models.ForumPosts;
    using Services.Contracts;
    using UniversityStudentSystem.Web.Controllers;

    public class ForumController : BaseController
    {
        private IForumService forumService;
        private ICategoryService categoriesService;

        public ForumController(IForumService forumService, ICategoryService categoryService)
        {
            this.forumService = forumService;
            this.categoriesService = categoryService;
        }
        
        // GET: Public/Forum
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPosts([DataSourceRequest] DataSourceRequest request)
        {
            var posts = this.forumService.GetAll()
                .OrderByDescending(f => f.Comments.Any() 
                                    ? f.Comments.OrderByDescending(c => c.CreatedOn).FirstOrDefault().CreatedOn 
                                    : f.CreatedOn)
                .To<ForumPostViewModel>().ToList();

            return this.Json(posts.ToDataSourceResult(request));
        }

        public ActionResult Details(int id)
        {
            var forum = this.forumService.GetAll().FirstOrDefault(f => f.Id == id);
            if (forum == null)
            {
                return this.Redirect("NotFound");
            }
            var forumViewModel = this.Mapper.Map<ForumPostViewModel>(forum);
            return this.View(forumViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(int id, CommentInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Details", new { id = id });
            }

            this.forumService.PostComment(model.Content, id, this.UserId);

            return this.RedirectToAction("Details", new { id = id });
        }

        [HttpGet]
        [Authorize]
        public ActionResult Post()
        {
            return this.View(new ForumInputModel
            {
                Categories = this.categoriesService.GetAll().ToList()
            });
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Post(ForumInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            int id = this.forumService.Create(model.Title, model.Content, model.CategoryId, this.UserId);

            return this.RedirectToAction("Details", new { id = id });
        }
    }
}