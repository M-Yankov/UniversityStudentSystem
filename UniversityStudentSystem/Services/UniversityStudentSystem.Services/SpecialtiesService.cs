namespace UniversityStudentSystem.Services
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using Data.Models;
    using Data.Repositories;
    using UniversityStudentSystem.Services.Contracts;

    public class SpecialtiesService : ISpecialtiesService
    {
        private IRepository<Specialty> specialtiesRepository;

        public SpecialtiesService(IRepository<Specialty> repository)
        {
            this.specialtiesRepository = repository;
        }

        public void DeleteById(int id)
        {
            var spec = this.specialtiesRepository.GetById(id);
            this.specialtiesRepository.Delete(spec);
            this.specialtiesRepository.Save();
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

            if (specialty == null)
            {
                return courses.AsQueryable();
            }

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
