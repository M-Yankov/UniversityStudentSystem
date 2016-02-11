namespace UniversityStudentSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CommonModels;
    using CustomAttributes;
    using UniversityStudentSystem.Common;

    public class Test : BaseModel<int>
    {
        private ICollection<Question> questions;

        public Test()
        {
            this.questions = new HashSet<Question>();
        }

        [Required]
        [DataRange(ModelConstants.MinDate, ModelConstants.MaxDate)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataRange(ModelConstants.MinDate, ModelConstants.MaxDate)]
        public DateTime EndDate { get; set; }

        [Required]
        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        public bool IsEnabled { get; set; }

        [Required]
        public int Points { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public virtual ICollection<Question> Questions
        {
            get
            {
                return this.questions;
            }
            set
            {
                this.questions = value;
            }
        }
    }
}