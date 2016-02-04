namespace UniversityStudentSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Common;

    public class Question
    {
        private ICollection<Answer> answers;

        public Question()
        {
            this.answers = new HashSet<Answer>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.ContentMaxLength)]
        public string Content { get; set; }

        [Required]
        public int Points { get; set; }

        public int TestId { get; set; }

        public virtual Test Test { get; set; }

        public virtual ICollection<Answer> Answers
        {
            get
            {
                return this.answers;
            }
            set
            {
                this.answers = value;
            }
        }
    }
}