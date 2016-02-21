namespace UniversityStudentSystem.Services
{
    using System;
    using Data.Models;
    using Data.Repositories;
    using UniversityStudentSystem.Services.Contracts;

    public class SemesterService : ISemesterService
    {
        private IRepository<Semester> semestersRepository;
        private IRepository<Course> coursesRepository;

        public SemesterService(IRepository<Semester> semestersRepo, IRepository<Course> coursesRepo)
        {
            this.semestersRepository = semestersRepo;
            this.coursesRepository = coursesRepo;
        }

        public int ChangeStatus(int id)
        {
            var semester = this.semestersRepository.GetById(id);
            semester.IsActive = !semester.IsActive;
            this.semestersRepository.Update(semester);
            this.semestersRepository.Save();
            return semester.SpecialtyId;
        }
    }
}
