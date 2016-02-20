namespace UniversityStudentSystem.Web.Areas.Trainer.Controllers
{
    using System.Web.Mvc;
    using Services.Contracts;
    using UniversityStudentSystem.Web.Controllers;
    using Web.Models.Messages;

    public class NotifyController : BaseController
    {
        private IMessageService messageService;

        public NotifyController(IMessageService messagesService)
        {
            this.messageService = messagesService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendToAll(int id, NotifyInputModel model)
        {
            this.messageService.SendToSpecialty(this.UserId, id, model.Content);

            return this.RedirectToAction("Details", "Specialties", new { id = id, area = "Public" });
        }
    }
}