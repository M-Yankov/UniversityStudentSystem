namespace UniversityStudentSystem.Web.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common;
    using Data.Models.CustomAttributes;

    public class TestInputModel
    {
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        [DataRange(ModelConstants.MinDate, ModelConstants.MaxDate)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataRange(ModelConstants.MinDate, ModelConstants.MaxDate)]
        public DateTime EndDate { get; set; }

        [Required]
        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        public bool? IsEnabled { get; set; }

        public int CourseId { get; set; }

        public IList<QuestionInputModel> Questions { get; set; }
    }
}