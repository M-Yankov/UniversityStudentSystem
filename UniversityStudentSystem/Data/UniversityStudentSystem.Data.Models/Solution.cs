namespace UniversityStudentSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common;
    using UniversityStudentSystem.Data.Models.CommonModels;

    public class Solution : BaseModel<int>
    {
        [Required]
        [MaxLength(ModelConstants.ContentMaxLength)]
        public string Path { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
