using System.ComponentModel.DataAnnotations;

namespace UniversityStudentSystem.Web.Models.Account
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}