namespace UniversityStudentSystem.Web.Models.Candidates
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Documents;
    using Specialties;
    using UniversityStudentSystem.Data.Models;
    using UniversityStudentSystem.Web.Infrastructure.Mapping;

    public class CandidateViewModel : IMapFrom<Candidate>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Specialty")]
        public string SpecialtyName { get; set; }

        public DateTime DateSent { get; set; }

        public bool IsApproved { get; set; }

        public bool IsRejected { get; set; }

        public ICollection<SpecialtyViewModel> Specialties { get; set; }

        public ICollection<DocumentViewModel> Documents { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Candidate, CandidateViewModel>()
                .ForMember(c => c.SpecialtyName, o => o.MapFrom(cand => cand.Specialty.Name));
        }
    }
}