namespace UniversityStudentSystem.Web.Models.Users
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using Common;
    using Data.Models;
    using UniversityStudentSystem.Web.Infrastructure.Mapping;

    public class UserInputModel : IMapFrom<User>
    {
        [Required]
        [Display(Name ="First name")]
        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.NameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name ="Lat name")]
        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.NameMaxLength)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Range(ModelConstants.MinAge, ModelConstants.MaxAge)]
        public int Age { get; set; }

        [Display(Name ="Facebook")]
        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.NameMaxLength)]
        [RegularExpression(ModelConstants.FacebookProfileRegularExpression)]
        public string FacebookAccount { get; set; }

        [Display(Name ="Skype")]
        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.NameMaxLength)]
        public string SkypeName { get; set; }

        [Display(Name ="LinkedIn")]
        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.NameMaxLength)]
        public string LinkedInProfile { get; set; }

        [MaxLength(ModelConstants.DescriptionMaxLength)]
        public string AboutMe { get; set; }
    }
}