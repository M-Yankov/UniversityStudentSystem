namespace UniversityStudentSystem.Services
{
    using System;
    using System.Linq;
    using Data;
    using Data.Models;
    using UniversityStudentSystem.Services.Contracts;

    public class SpecialtiesService : ISpecialtiesService
    {
        public IRepository<Specialty> specialtiesRepository;

        public SpecialtiesService(IRepository<Specialty> repository)
        {
            this.specialtiesRepository = repository;
        }

        public IQueryable<Specialty> GetAll(int count)
        {
            return this.specialtiesRepository.All().Take(count);
        }
    }
}
