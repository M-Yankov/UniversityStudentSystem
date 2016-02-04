namespace UniversityStudentSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class CourseTask
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.DescriptionMaxLength)]
        public string Requirements { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
    }
}