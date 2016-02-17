namespace UniversityStudentSystem.Web.Areas.Admin.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class AnswerInputModel
    {
        [Required]
        [MaxLength(ModelConstants.NameMaxLength)]
        public string Content { get; set; }

        [Required]
        public bool IsRight { get; set; }

        public int QuestionId { get; set; }
    }
}