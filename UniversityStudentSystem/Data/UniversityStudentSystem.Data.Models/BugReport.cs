namespace UniversityStudentSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common;
    using UniversityStudentSystem.Data.Models.CommonModels;

    public class BugReport : BaseModel<int>
    {
        [Required]
        [MaxLength(ModelConstants.DescriptionMaxLength)]
        public string Content { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
