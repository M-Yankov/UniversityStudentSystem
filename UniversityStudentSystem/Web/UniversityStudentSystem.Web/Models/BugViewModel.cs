namespace UniversityStudentSystem.Web.Models
{
    using UniversityStudentSystem.Data.Models;
    using UniversityStudentSystem.Web.Infrastructure.Mapping;

    public class BugViewModel : IMapFrom<BugReport>
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string Email { get; set; }
    }
}