namespace UniversityStudentSystem.Web.Models.Specialties
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Data.Models;
    using Semesters;
    using UniversityStudentSystem.Web.Infrastructure.Mapping;

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