namespace UniversityStudentSystem.Web.Models.Semesters
{
    using Data.Models;
    using UniversityStudentSystem.Web.Infrastructure.Mapping;

    public class SemesterViewModel : IMapFrom<Semester>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Fee { get; set; }

        public bool IsActive { get; set; }
    }
}