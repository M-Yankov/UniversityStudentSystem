namespace UniversityStudentSystem.Services
{
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

        public void Create(Test testToAdd , int courseId)
        {
            // object data is changed by reference.
            var questions = testToAdd.Questions.ToList();
            for (int i = 0; i < questions.Count; i++)
            {
                var answers = questions[i].Answers.ToList();
                int indexOfCorrectAnswer = questions[i].Index;
                answers[indexOfCorrectAnswer].IsRight = true;
            }
        }
    }
}
