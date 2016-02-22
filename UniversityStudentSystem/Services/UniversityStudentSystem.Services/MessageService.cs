
namespace UniversityStudentSystem.Services
{
    using System;
    using System.Linq;
    using Data.Models;
    using Data.Repositories;
    using UniversityStudentSystem.Services.Contracts;

    public class MessageService : IMessageService
    {
        private IRepository<Message> messagesRepository;
        private IRepository<Specialty> specialtiesRepository;

        public MessageService(IRepository<Message> messagesRepo, IRepository<Specialty> specialtiesRepo)
        {
            this.messagesRepository = messagesRepo;
            this.specialtiesRepository = specialtiesRepo;
        }

        public IQueryable<Message> GetMessagesForUser(string userId)
        {
            return this.messagesRepository.All().Where(m => m.ReceiverId == userId || m.SenderId == userId);
        }

        public void SendToSpecialty(string senderId, int specialtyId, string content)
        {
            var specialty = this.specialtiesRepository.GetById(specialtyId);

            foreach (var user in specialty.Students)
            {
                this.messagesRepository.Add(new Message()
                {
                    Content = content,
                    CreatedOn = DateTime.Now,
                    DateSent = DateTime.Now,
                    ReceiverId = user.Id,
                    SenderId = senderId
                });
            }

            this.messagesRepository.Save();
        }
    }
}
