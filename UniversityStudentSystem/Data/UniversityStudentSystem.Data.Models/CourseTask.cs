namespace UniversityStudentSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CourseTask
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(15000)]
        public string Requirements { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
    }
}