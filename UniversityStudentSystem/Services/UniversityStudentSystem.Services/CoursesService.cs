namespace UniversityStudentSystem.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data.Models;
    using Data.Repositories;
    using UniversityStudentSystem.Services.Contracts;

    public class CoursesService : ICoursesService
    {
        private IRepository<Course> coursesRepository;
        private IRepository<User, string> trainersRepository;
        private IRepository<TestResult> testResultsRepository;

        public CoursesService(
            IRepository<Course> coursesRepo,
            IRepository<User, string> trainersRepo,
            IRepository<TestResult> testResultRepo )
        {
            this.coursesRepository = coursesRepo;
            this.trainersRepository = trainersRepo;
            this.testResultsRepository = testResultRepo;
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

        public int AddCourse(string name, string description, int semesterId)
        {
            var course = new Course()
            {
                Name = name,
                Description = description,
                SemesterId = semesterId
            };

            this.coursesRepository.Add(course);
            this.coursesRepository.Save();

            return course.Id;
        }

        public void Edit(Course model)
        {
            this.coursesRepository.Update(model);
            this.coursesRepository.Save();
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

        public void AddResourse(string name, string path, int courseId)
        {
            var course = this.coursesRepository.GetById(courseId);
            course.Resources.Add(new Resource() { Name = name, Path = path });
            this.coursesRepository.Update(course);
            this.coursesRepository.Save();
        }

        public string IsAllowed(string userId, int courseId)
        {
            var course = this.coursesRepository.GetById(courseId);

            if (!course.Semester.IsActive)
            {
                return "Semester for this course is not active";
            }

            if (!course.Semester.Specialty.Students.Any(s => s.Id == userId))
            {
                return "You are not allowed to view course details. You may send a candidature for " + course.Semester.Specialty.Name + ".";
            }

            return null;
        }

        public void SaveSolution(string path, string userId, int courseId)
        {
            var course = this.coursesRepository.GetById(courseId);
            course.Solutions.Add(new Solution() { UserId = userId, Path = path, CourseId = courseId });

            this.coursesRepository.Update(course);
            this.coursesRepository.Save();
        }

        public string SolutionResult(string userId, int courseId)
        {
             var course = this.coursesRepository.GetById(courseId);
             var solution =  course.Solutions
                .OrderByDescending(s => s.CreatedOn)
                .FirstOrDefault(s => s.UserId == userId);

            if (solution != null)
            {
                return solution.Path;
            }

            return null;
        }

        public void AddMark(int value, string username, int courseId, string reason)
        {
            var student = trainersRepository.All().FirstOrDefault(u => u.UserName == username);
            student.Marks.Add(new Mark()
            {
                CourseId = courseId,
                Reason = reason,
                Value = value,
                CreatedOn = DateTime.Now
            });

            this.trainersRepository.Update(student);
            this.trainersRepository.Save();
        }

        public Test GetTestForStudent(int courseId, string userId)
        {
            var course = this.coursesRepository.GetById(courseId);
            var test = course.Tests.FirstOrDefault(t => 
                t.StartDate < DateTime.Now && 
                DateTime.Now < t.EndDate && 
                t.IsEnabled && 
                (!t.TestResults.Any(tr => tr.UserId == userId) || t.TestResults.Count == 0));

            return test;
        }

        public TestResult SolveTest(int courseId, string userId, int testId, IList<int> indexAnswers)
        {
            var questions = this.coursesRepository
                .GetById(courseId)
                .Tests
                .FirstOrDefault(t => t.Id == testId)
                .Questions;

            int total = 0;
            int result = 0;
            int counter = 0;
            foreach (var question in questions)
            {
                if (question.Index == indexAnswers[counter])
                {
                    result += question.Points;
                }

                total += question.Points;
                counter++;
            }

            var testResult = new TestResult()
            {
                Result = result,
                UserId = userId,
                TestId = testId,
                Total = total
            };

            this.testResultsRepository.Add(testResult);
            this.testResultsRepository.Save();

            return testResult;
        }

        public TestResult GetResult(int id)
        {
            return this.testResultsRepository.GetById(id);
        }

        public void DeleteById(int id)
        {
            var course = this.coursesRepository.GetById(id);
            this.coursesRepository.Delete(course);
            this.coursesRepository.Save();
        }
    }
}
