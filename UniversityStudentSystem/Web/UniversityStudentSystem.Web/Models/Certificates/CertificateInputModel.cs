namespace UniversityStudentSystem.Web.Models.Certificates
{
    using System.ComponentModel.DataAnnotations;

    public class CertificateInputModel
    {
        public string UserId { get; set; }

        [Required]
        public int SpecialtyId { get; set; }
    }
}