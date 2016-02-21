namespace UniversityStudentSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common;
    using CommonModels;

    public class Mark : BaseModel<int>
    {
        [Required]
        [Range(ModelConstants.MarkMinValue, ModelConstants.MarkMaxValue)]
        public int Value { get; set; }

        [MaxLength(ModelConstants.ContentMaxLength)]
        public string Reason { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
    }
}
