namespace UniversityStudentSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using CommonModels;
    using UniversityStudentSystem.Common;

    public class Answer : BaseModel<int>
    {
        [Required]
        [MaxLength(ModelConstants.NameMaxLength)]
        public string Content { get; set; }

        [Required]
        public bool IsRight { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}