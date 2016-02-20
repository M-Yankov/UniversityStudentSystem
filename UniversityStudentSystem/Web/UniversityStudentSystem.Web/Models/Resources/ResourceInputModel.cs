namespace UniversityStudentSystem.Web.Models.Resources
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using UniversityStudentSystem.Common;

    public class ResourceInputModel
    {
        [Required]
        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        public HttpPostedFileBase File { get; set; }
    }
}