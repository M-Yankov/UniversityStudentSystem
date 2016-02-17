namespace UniversityStudentSystem.Services.Contracts
{
    using UniversityStudentSystem.Data.Models;

    public interface ITestService
    {
        void Create(Test testToAdd, int courseId);
    }
}
