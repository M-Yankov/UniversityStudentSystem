namespace UniversityStudentSystem.Web.Areas.Admin.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using Common;
    using Data.Models;
    using Infrastructure.Mapping;

    public class TestAdminModel : IMapFrom<Test>
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [UIHint("DateTime")]
        public DateTime StartDate { get; set; }

        [UIHint("DateTime")]
        public DateTime EndDate { get; set; }

        [Required]
        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        public bool IsEnabled { get; set; }
    }
}