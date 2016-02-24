namespace UniversityStudentSystem.Services.Contracts
{
    using System.Linq;

    using UniversityStudentSystem.Data.Models;

    public interface IMessageService
    {
        void SendToSpecialty(string senderId, int specialtyId, string content);

        IQueryable<Message> GetMessagesForUser(string userId);
    }
}
