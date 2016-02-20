
namespace UniversityStudentSystem.Web.Models.CoursesTask
{
    using Infrastructure.Mapping;
    using UniversityStudentSystem.Data.Models;

    public class TaskViewModel : IMapFrom<CourseTask>
    {
        public int Id { get; set; }

        public string Requirements { get; set; }
    }
}