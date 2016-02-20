namespace UniversityStudentSystem.Web.Hubs
{
    using Microsoft.AspNet.SignalR;

    public class Chatter : Hub
    {
        public void SendMessage(string message)
        {
            this.Clients.All.addMessage(message, this.Context.User.Identity.Name);
        }
    }
}