namespace UniversityStudentSystem.Web.Areas.Trainer.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Common;
    using Data.Models;
    using Infrastructure.Mapping;

    public class AnswerInputModel : IMapTo<Answer>
    {
        [Required]
        [MaxLength(ModelConstants.NameMaxLength)]
        public string Content { get; set; }

        [Required]
        public bool IsRight { get; set; }

        public int QuestionId { get; set; }
    }
}