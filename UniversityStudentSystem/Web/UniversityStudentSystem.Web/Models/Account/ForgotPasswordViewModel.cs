﻿namespace UniversityStudentSystem.Web.Models.Account
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
