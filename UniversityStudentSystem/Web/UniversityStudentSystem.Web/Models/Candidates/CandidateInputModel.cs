namespace UniversityStudentSystem.Web.Models.Candidates
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using UniversityStudentSystem.Web.Models.Specialties;

    public class CandidateInputModel
    {
        [Required]
        public int SpecialtyId { get; set; }

        public ICollection<SpecialtyViewModel> Specialties { get; set; }
    }
}