

namespace UniversityStudentSystem.Services.Contracts
{
    public interface IMessageService
    {
        void SendToSpecialty(string senderId, int specialtyId, string content);
    }
}
