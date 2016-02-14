namespace UniversityStudentSystem.Services
{
    using System;
    using System.Linq;
    using Data.Repositories;
    using Data.Models;
    using UniversityStudentSystem.Services.Contracts;

    public class SpecialtiesService : ISpecialtiesService
    {
        public IRepository<Specialty> specialtiesRepository;

        public SpecialtiesService(IRepository<Specialty> repository)
        {
            this.specialtiesRepository = repository;
        }

        public IQueryable<Specialty> GetAll()
        {
            return this.specialtiesRepository.All();
        }
    }
}
