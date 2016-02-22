namespace UniversityStudentSystem.Web.Models.Marks
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Data.Models;
    using UniversityStudentSystem.Web.Infrastructure.Mapping;
    using Users;
    using System.Linq;

    public class MarkViewModel : IMapFrom<Mark>, IHaveCustomMappings
    {
        public int Value { get; set; }

        public string Reason { get; set; }

        public string Username { get; set; }

        public string UserId { get; set; }

        public int CourseId { get; set; }

        public string Course { get; set; }

        public IList<string> Trainers { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Mark, MarkViewModel>()
                .ForMember(c => c.Username, o => o.MapFrom(m => m.User.UserName))
                .ForMember(c => c.Course, o => o.MapFrom(m => m.Course.Name))
                .ForMember(
                    c => c.Trainers,
                    o => o.MapFrom(m => m.Course.Trainers.Select(t => t.FirstName + " " + t.LastName)));
        }
    }
}