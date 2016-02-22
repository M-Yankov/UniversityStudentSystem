namespace UniversityStudentSystem.Web.Models.Certificates
{
    using System;
    using AutoMapper;
    using Data.Models;
    using UniversityStudentSystem.Web.Infrastructure.Mapping;

    public class CertificateViewModel : IMapFrom<Diploma>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public int SpecialtyId { get; set; }

        public string Specialty { get; set; }

        public string PathToImage { get; set; }

        public string UserId { get; set; }

        public string FullNameStudent { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Diploma, CertificateViewModel>()
                .ForMember(c => c.Specialty, o => o.MapFrom(d => d.Specialty.Name))
                .ForMember(c => c.FullNameStudent, o => o.MapFrom(d => d.User.FirstName + " " + d.User.LastName));
        }
    }
}