
namespace UniversityStudentSystem.Web.Areas.Admin.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class QuestionInputModel
    {
        [Required]
        [DataType(DataType.MultilineText)]
        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.ContentMaxLength)]
        public string Content { get; set; }

        [Required]
        public int Points { get; set; }

        [Required]
        public int Index { get; set; }

        public int TestId { get; set; }

        public IList<AnswerInputModel> Answers { get; set; }
    }
}