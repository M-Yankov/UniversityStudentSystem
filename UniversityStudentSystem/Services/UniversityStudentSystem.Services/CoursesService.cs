
namespace UniversityStudentSystem.Services
{
    using System;
    using System.Linq;
    using Data.Models;
    using Data.Repositories;
    using UniversityStudentSystem.Services.Contracts;

    public class CoursesService : ICoursesService
    {
        private IRepository<Course> coursesRepository;

        public CoursesService(IRepository<Course> repository)
        {
            this.coursesRepository = repository;
        }

        public IQueryable<Course> GetAll()
        {
            return this.coursesRepository.All();
        }
    }
}
