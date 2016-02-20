namespace UniversityStudentSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common;
    using UniversityStudentSystem.Data.Models.CommonModels;

    public class Resource : BaseModel<int>
    {
        [Required]
        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(ModelConstants.DescriptionMaxLength)]
        public string Path { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
    }
}
