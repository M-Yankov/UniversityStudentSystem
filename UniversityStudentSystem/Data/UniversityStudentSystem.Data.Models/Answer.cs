namespace UniversityStudentSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using UniversityStudentSystem.Common;

    public class Answer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ModelConstants.NameMaxLength)]
        public string Content { get; set; }

        [Required]
        public bool IsRight { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}