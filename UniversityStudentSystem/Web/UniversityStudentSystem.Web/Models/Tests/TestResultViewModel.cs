namespace UniversityStudentSystem.Web.Models.Tests
{
    using System;
    using AutoMapper;
    using Data.Models;
    using UniversityStudentSystem.Web.Infrastructure.Mapping;

    public class TestResultViewModel : IMapFrom<TestResult>, IHaveCustomMappings
    {
        public int Result { get; set; }

        public int Total { get; set; }

        public string UserId { get; set; }

        public string User { get; set; }

        public int TestId { get; set; }

        public string Test { get; set; }

        public string Course { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<TestResult, TestResultViewModel>()
                .ForMember(c => c.Course, o => o.MapFrom(t => t.Test.Course.Name))
                .ForMember(c => c.Test, o => o.MapFrom(t => t.Test.Name))
                .ForMember(c => c.User, o => o.MapFrom(t => t.User.FirstName + " " + t.User.LastName));
        }
    }
}