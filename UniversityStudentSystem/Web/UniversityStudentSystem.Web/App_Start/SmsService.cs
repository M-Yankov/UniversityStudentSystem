namespace UniversityStudentSystem.Web
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            return Task.FromResult(0);
        }
    }
}