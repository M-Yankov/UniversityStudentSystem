namespace UniversityStudentSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Answer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Content { get; set; }

        [Required]
        public bool IsRight { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}