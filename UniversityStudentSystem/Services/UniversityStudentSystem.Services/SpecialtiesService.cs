namespace UniversityStudentSystem.Services
{
    using System;
    using System.Linq;
    using Data.Repositories;
    using Data.Models;
    using UniversityStudentSystem.Services.Contracts;
    using System.Collections.Generic;
    using System.Data.Entity;
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

        public IQueryable<Course> GetAllCoursesForUser(string id)
        {
            var specialty = this.specialtiesRepository
                .All()
                .Where(s => s.Students.Any(std => std.Id == id))
                .Include(s => s.Semesters)
                .FirstOrDefault();

            var courses = new List<Course>();

            foreach (var item in specialty.Semesters)
            {
                foreach (var e in item.Courses)
                {
                    courses.Add(e);
                }
            }

            return courses.AsQueryable();
        }
    }
}
