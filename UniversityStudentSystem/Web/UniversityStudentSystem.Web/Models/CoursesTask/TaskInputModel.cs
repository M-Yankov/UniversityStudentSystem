namespace UniversityStudentSystem.Web.Models.CoursesTask
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using Common;
    using Data.Models;
    using UniversityStudentSystem.Web.Infrastructure.Mapping;

    public class TaskInputModel : IMapTo<CourseTask>
    {
        [Required]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.DescriptionMaxLength)]
        public string Requirements { get; set; }
    }
}