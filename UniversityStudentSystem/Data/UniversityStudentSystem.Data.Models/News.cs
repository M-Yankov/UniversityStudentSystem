namespace UniversityStudentSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class News
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.NameMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(ModelConstants.NameMinLength)]
        public string Content { get; set; }

        [Required]
        [DataRange(ModelConstants.MinDate, ModelConstants.MaxDate)]
        public DateTime DatePosted { get; set; }

        [Required]
        [MaxLength(ModelConstants.ContentMaxLength)]
        public string PhotoPath { get; set; }
    }
}
