namespace UniversityStudentSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Infrastructure.Mapping;
    using Models.Courses;
    using Models.Messages;
    using Services.Contracts;

    public class ProfileController : BaseController
    {
        private ISpecialtiesService specialtyService;
        private IUserService userService;
        private IMessageService messageService;

        public ProfileController(
            ISpecialtiesService specialtyService,
            IUserService userService,
            IMessageService messageService)
        {
            this.specialtyService = specialtyService;
            this.userService = userService;
            this.messageService = messageService;
        }

        public ActionResult Messages()
        {
            var messages = this.messageService.GetMessagesForUser(this.UserId).To<MessageViewModel>().ToList();
            return View(messages);
        }

        public ActionResult Courses()
        {
            var courses = this.specialtyService.GetAllCoursesForUser(this.UserId).To<CourseViewModel>().ToList();
            return this.View(courses);
        }
    }
}