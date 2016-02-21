namespace UniversityStudentSystem.Web.Areas.Public.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Common;
    using Infrastructure.Mapping;
    using Models.Users;
    using Services.Contracts;
    using Web.Controllers;

    public class TrainersController : BaseController
    {
        private IUserService usersService;

        public TrainersController(IUserService usersService)
        {
            this.usersService = usersService;
        }

        public ActionResult Index()
        {
            var trainerRole = usersService.GetRoles().FirstOrDefault(r => r.Name == RoleConstants.Trainer);

            var trainers = usersService
                       .GetAll()
                       .Where(u => u.Roles.Any(r => r.RoleId == trainerRole.Id))
                       .To<UserViewModel>()
                       .ToList();

            return View(trainers);
        }

        public ActionResult Details(string id)
        {
            var trainerRole = usersService.GetRoles().FirstOrDefault(r => r.Name == RoleConstants.Trainer);

            var trainerDetails = usersService
                       .GetAll()
                       .FirstOrDefault(u => u.Roles.Any(r => r.RoleId == trainerRole.Id) && u.Id == id);

            var viewModel = this.Mapper.Map<UserViewModel>(trainerDetails);

            return View(viewModel);
        }
    }
}