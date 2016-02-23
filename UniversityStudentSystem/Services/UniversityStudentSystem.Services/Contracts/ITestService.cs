namespace UniversityStudentSystem.Services.Contracts
{
    using System;
    using System.Linq;
    using UniversityStudentSystem.Data.Models;

    public interface ITestService
    {
        IQueryable<Test> GetAll();

        void Create(Test testToAdd, int courseId);

        void DeleteById(int id);

        void Update(int id, DateTime startDate, DateTime endDate, string name, bool isEnabled);
    }
}
