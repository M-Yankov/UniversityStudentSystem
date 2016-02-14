namespace UniversityStudentSystem.Web.Models.Users
{
    using System;
    using AutoMapper;
    using UniversityStudentSystem.Data.Models;
    using UniversityStudentSystem.Web.Infrastructure.Mapping;

    public class UserViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public Genre Genre { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }

        public string FacebookAccount { get; set; }

        public string SkypeName { get; set; }

        public string LinkedInProfile { get; set; }

        public long FacultyNumber { get; set; }

        public string AvaratUrl { get; set; }

        public string AboutMe { get; set; }

        public DateTime DateRegistered { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<User, UserViewModel>()
                .ForMember(c => c.FullName, o => o.MapFrom(u => u.FirstName + " " + u.LastName));
        }
    }
}