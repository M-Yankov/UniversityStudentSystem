namespace UniversityStudentSystem.Services.Contracts
{
    using UniversityStudentSystem.Data.Models;
    using System.Linq;
    using System.Collections.Generic;

    public interface IMessageService
    {
        void SendToSpecialty(string senderId, int specialtyId, string content);

        IQueryable<Message> GetMessagesForUser(string userId);
    }
}
