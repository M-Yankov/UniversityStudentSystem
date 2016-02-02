using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniversityStudentSystem.Data.Models
{
    public class Test
    {
        private ICollection<Question> questions;

        public Test()
        {
            this.questions = new HashSet<Question>();
        }

        [Required]
        [DataRange("01.01.2016", "01.01.2030")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataRange("01.01.2016", "01.01.2030")]
        public DateTime EndDate { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
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