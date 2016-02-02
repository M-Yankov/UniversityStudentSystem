namespace UniversityStudentSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class News
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MinLength(3)]
        public string Content { get; set; }

        [Required]
        [DataRange("01.01.2016", "01.01.2030")]
        public DateTime DatePosted { get; set; }

        [Required]
        [MaxLength(500)]
        public string PhotoPath { get; set; }
    }
}
