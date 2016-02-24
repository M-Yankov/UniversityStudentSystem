namespace UniversityStudentSystem.Services
{
    using System;
    using System.Linq;
    using Data.Models;
    using UniversityStudentSystem.Data.Repositories;
    using UniversityStudentSystem.Services.Contracts;

    public class TestService : ITestService
    {
        private IRepository<Test> testRepository;

        public TestService(IRepository<Test> testRepo)
        {
            this.testRepository = testRepo;
        }

        public void Create(Test testToAdd, int courseId)
        {
            // object data is changed by reference.
            var questions = testToAdd.Questions.ToList();
            for (int i = 0; i < questions.Count; i++)
            {
                var answers = questions[i].Answers.ToList();
                int indexOfCorrectAnswer = questions[i].Index;
                answers[indexOfCorrectAnswer].IsRight = true;
            }

            testToAdd.CourseId = courseId;

            this.testRepository.Add(testToAdd);
            this.testRepository.Save();
        }

        public void DeleteById(int id)
        {
            var test = this.testRepository.GetById(id);
            this.testRepository.Delete(test);
            this.testRepository.Save();
        }

        public IQueryable<Test> GetAll()
        {
            return this.testRepository.All();
        }

        public void Update(int id, DateTime startDate, DateTime endDate, string name, bool isEnabled)
        {
            var test = this.testRepository.GetById(id);
            test.Name = name;
            test.StartDate = startDate;
            test.EndDate = endDate;
            test.IsEnabled = isEnabled;

            this.testRepository.Update(test);
            this.testRepository.Save();
        }
    }
}
