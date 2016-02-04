namespace UniversityStudentSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.NameMaxLength)]
        public string Name { get; set; }
    }
}