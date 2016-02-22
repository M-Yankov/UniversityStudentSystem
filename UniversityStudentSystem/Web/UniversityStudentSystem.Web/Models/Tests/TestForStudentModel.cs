namespace UniversityStudentSystem.Web.Models.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using AutoMapper;
    using Data.Models;
    using UniversityStudentSystem.Web.Infrastructure.Mapping;

    public class TestForStudentModel : IMapFrom<Test>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsEnabled { get; set; }

        public int CourseId { get; set; }

        public string Course { get; set; }

        public string Name { get; set; }

        public IList<QuestionForStudentModel> Questions { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Test, TestForStudentModel>()
                .ForMember(t => t.Course, o => o.MapFrom(test => test.Course.Name));
        }
    }
}