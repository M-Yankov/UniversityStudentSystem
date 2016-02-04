
namespace UniversityStudentSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class Mark
    {
        public int Id { get; set; }

        [Required]
        [Range(ModelConstants.MarkMinValue, ModelConstants.MarkMaxValue)]
        public int Value { get; set; }

        [MaxLength(ModelConstants.ContentMaxLength)]
        public string Reason { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int TestId { get; set; }

        public virtual Test Test { get; set; }
    }
}
