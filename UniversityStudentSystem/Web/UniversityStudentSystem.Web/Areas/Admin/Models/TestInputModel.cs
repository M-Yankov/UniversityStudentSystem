namespace UniversityStudentSystem.Web.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Common;
    using Data.Models;
    using Data.Models.CustomAttributes;
    using Infrastructure.Mapping;

    public class TestInputModel : IMapTo<Test>
    {
        [Required]
        [Display(Name = "Start date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        [DataRange(ModelConstants.MinDate, ModelConstants.MaxDate)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End date")]
        [DataRange(ModelConstants.MinDate, ModelConstants.MaxDate)]
        public DateTime EndDate { get; set; }

        [Required]
        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Enabled")]
        public bool? IsEnabled { get; set; }

        public int CourseId { get; set; }

        public IList<QuestionInputModel> Questions { get; set; }
    }
}