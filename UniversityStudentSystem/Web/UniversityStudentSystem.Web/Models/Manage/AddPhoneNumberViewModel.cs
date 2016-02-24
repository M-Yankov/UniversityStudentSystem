namespace UniversityStudentSystem.Web.Models.Manage
{
    using System.ComponentModel.DataAnnotations;

    public class AddPhoneNumberViewModel
    {
        [Phone]
        [Required]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }
}