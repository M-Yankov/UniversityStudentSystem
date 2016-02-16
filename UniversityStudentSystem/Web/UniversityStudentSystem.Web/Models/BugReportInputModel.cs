namespace UniversityStudentSystem.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using UniversityStudentSystem.Common;

    public class BugReportInputModel
    {
        [Required]
        [DataType(DataType.MultilineText)]
        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.DescriptionMaxLength)]
        public string Content { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}