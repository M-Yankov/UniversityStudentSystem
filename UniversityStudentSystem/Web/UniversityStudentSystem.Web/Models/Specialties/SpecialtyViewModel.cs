namespace UniversityStudentSystem.Web.Models.Specialties
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Data.Models;
    using System.Linq;
    using UniversityStudentSystem.Web.Infrastructure.Mapping;

    public class SpecialtyViewModel : IMapFrom<Specialty>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<string> Semesters { get; set; }

        public int StudentsCount { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Specialty, SpecialtyViewModel>()
                .ForMember(s => s.Semesters, o => o.MapFrom(spec => spec.Semesters.Select(sem => sem.Name)))
                .ForMember(s => s.StudentsCount, o => o.MapFrom(spec => spec.Students.Count));
        }
    }
}