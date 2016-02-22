namespace UniversityStudentSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using UniversityStudentSystem.Data.Models.CommonModels;

    public class TestResult : BaseModel<int>
    {
        [Required]
        public int Result { get; set; }

        [Required]
        public int Total { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int TestId { get; set; }

        public virtual Test Test { get; set; }
    }
}
