namespace UniversityStudentSystem.Web.Models.Messages
{
    using UniversityStudentSystem.Web.Infrastructure.Mapping;
    using UniversityStudentSystem.Data.Models;
    using System;
    using AutoMapper;

    public class MessageViewModel : IMapFrom<Message>, IHaveCustomMappings
    {
        public DateTime DateSent { get; set; }

        public bool IsViewed { get; set; }

        public string Content { get; set; }

        public string SenderId { get; set; }

        public string Sender { get; set; }

        public string ReceiverId { get; set; }

        public string Receiver { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Message, MessageViewModel>()
                .ForMember(c => c.Sender, o => o.MapFrom(m => m.Sender.FirstName + " " + m.Sender.LastName))
                .ForMember(c => c.Receiver, o => o.MapFrom(m => m.Receiver.FirstName + " " + m.Receiver.LastName));
        }
    }
}