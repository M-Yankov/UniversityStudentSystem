
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

        public void AddTask(CourseTask task, int id)
        {
            var course = this.coursesRepository.GetById(id);
            if (course == null)
            {
                return;
            }

            course.Tasks.Add(task);
            coursesRepository.Update(course);
            coursesRepository.Save();
        }

        public IQueryable<Course> GetAll()
        {
            return this.coursesRepository.All();
        }
    }
}
