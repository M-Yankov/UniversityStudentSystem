namespace UniversityStudentSystem.Web.Models.Specialties
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Data.Models;
    using System.Linq;
    using UniversityStudentSystem.Web.Infrastructure.Mapping;
    using Semesters;
    using System.ComponentModel.DataAnnotations;
    public class SpecialtyViewModel : IMapFrom<Specialty>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [UIHint("Name")]
        public string Name { get; set; }

        [UIHint("Description")]
        public string Description { get; set; }

        public ICollection<SemesterViewModel> Semesters { get; set; }

        public int StudentsCount { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Specialty, SpecialtyViewModel>()
                .ForMember(s => s.StudentsCount, o => o.MapFrom(spec => spec.Students.Count));
        }
    }
}