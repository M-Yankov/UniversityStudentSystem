
namespace UniversityStudentSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
    public class Mark
    {
        public int Id { get; set; }

        [Required]
        [Range(2, 6)]
        public int Value { get; set; }

        [MaxLength(500)]
        public string Reason { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int TestId { get; set; }

        public virtual Test Test { get; set; }
    }
}
