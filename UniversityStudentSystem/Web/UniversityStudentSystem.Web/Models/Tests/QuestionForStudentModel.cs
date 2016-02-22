namespace UniversityStudentSystem.Web.Models.Tests
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using UniversityStudentSystem.Web.Infrastructure.Mapping;

    public class QuestionForStudentModel : IMapFrom<Question>
    {
        public string Content { get; set; }

        public int Points { get; set; }

        [Required]
        public int? Index { get; set; }

        public IList<AnswerForStudentModel> Answers { get; set; }
    }
}