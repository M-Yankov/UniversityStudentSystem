﻿namespace UniversityStudentSystem.Services
{
    using System;
    using System.Linq;
    using Data.Models;
    using Data.Repositories;
    using UniversityStudentSystem.Services.Contracts;

    public class CoursesService : ICoursesService
    {
        private IRepository<Course> coursesRepository;
        private IRepository<User, string> trainersRepository;

        public CoursesService(IRepository<Course> coursesRepo, IRepository<User, string> trainersRepo)
        {
            this.coursesRepository = coursesRepo;
            this.trainersRepository = trainersRepo;
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

        public void JoinIn(int courseId, string userId)
        {
            var course = this.coursesRepository.GetById(courseId);
            if (course.Trainers.Any(t => t.Id == userId))
            {
                return;
            }

            var trainer = this.trainersRepository.GetById(userId);
            course.Trainers.Add(trainer);
            this.coursesRepository.Update(course);
            this.coursesRepository.Save();
        }
    }
}
