namespace UniversityStudentSystem.Web.Models.Courses
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Common;
    using Semesters;
    using UniversityStudentSystem.Data.Models;
    using UniversityStudentSystem.Web.Infrastructure.Mapping;

    public class CourseInputModel : IMapTo<Course>, IMapFrom<Course>
    {
        [Required]
        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.NameMaxLength)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.DescriptionMaxLength)]
        public string Description { get; set; }
        
        [Required]
        public int SemesterId { get; set; }

        public string SpecialtyName { get; set; }

        public ICollection<SemesterViewModel> Semesters { get; set; }
    }
}