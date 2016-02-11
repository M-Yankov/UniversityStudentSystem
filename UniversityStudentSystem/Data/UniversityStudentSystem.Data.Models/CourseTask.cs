namespace UniversityStudentSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common;
    using CommonModels;

    public class CourseTask : BaseModel<int>
    {
        [Required]
        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.DescriptionMaxLength)]
        public string Requirements { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
    }
}