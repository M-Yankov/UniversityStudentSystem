namespace UniversityStudentSystem.Web.Models.Tests
{
    using Data.Models;
    using UniversityStudentSystem.Web.Infrastructure.Mapping;

    public class AnswerForStudentModel : IMapFrom<Answer>
    {
        public string Content { get; set; }

        public bool IsRight { get; set; }
    }
}