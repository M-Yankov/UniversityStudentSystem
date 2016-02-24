namespace UniversityStudentSystem.Web
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;

    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            return Task.FromResult(0);
        }
    }
}