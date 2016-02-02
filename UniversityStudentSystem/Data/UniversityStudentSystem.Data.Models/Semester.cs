namespace UniversityStudentSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Semester
    {
        private ICollection<Course> courses;

        public Semester()
        {
            this.courses = new HashSet<Course>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [DataRange("01.01.2016", "01.01.2030")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataRange("01.01.2016", "01.01.2030")]
        public DateTime EndDate { get; set; }

        public int SpecialtyId { get; set; }

        public virtual Specialty Specialty { get; set; }

        public virtual ICollection<Course> Courses
        {
            get
            {
                return this.courses;
            }
            set
            {
                this.courses = value;
            }
        }
    }
}