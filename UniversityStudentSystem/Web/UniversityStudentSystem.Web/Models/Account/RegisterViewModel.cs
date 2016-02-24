namespace UniversityStudentSystem.Web.Models.Account
{
    using System.ComponentModel.DataAnnotations;
    using UniversityStudentSystem.Common;
    using UniversityStudentSystem.Data.Models;

    public class RegisterViewModel
    {
        [Required]
        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.NameMaxLength)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.NameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.NameMaxLength)]
        public string LastName { get; set; }

        [Required]
        public Genre Genre { get; set; }

        [Required]
        [Range(ModelConstants.MinAge, ModelConstants.MaxAge)]
        public int Age { get; set; }

        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.NameMaxLength)]
        [RegularExpression(ModelConstants.FacebookProfileRegularExpression)]
        public string FacebookAccount { get; set; }

        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.NameMaxLength)]
        public string SkypeName { get; set; }

        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.NameMaxLength)]
        public string LinkedInProfile { get; set; }

        [MaxLength(ModelConstants.DescriptionMaxLength)]
        public string AboutMe { get; set; }
    }
}