namespace UniversityStudentSystem.Web.Models.Marks
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Data.Models;
    using UniversityStudentSystem.Web.Infrastructure.Mapping;
    using Users;

    public class MarkViewModel : IMapFrom<Mark>, IHaveCustomMappings
    {
        public int Value { get; set; }

        public string Reason { get; set; }

        public string Username { get; set; }

        public string UserId { get; set; }

        public int CourseId { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Mark, MarkViewModel>()
                .ForMember(c => c.Username, o => o.MapFrom(m => m.User.UserName));
        }
    }
}