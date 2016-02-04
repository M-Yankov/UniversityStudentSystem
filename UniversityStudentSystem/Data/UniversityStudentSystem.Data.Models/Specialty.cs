namespace UniversityStudentSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class Specialty
    {
        private ICollection<User> students;
        private ICollection<Diploma> diploms;
        private ICollection<Semester> semesters;

        public Specialty()
        {
            this.students = new HashSet<User>();
            this.diploms = new HashSet<Diploma>();
            this.semesters = new HashSet<Semester>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.DescriptionMaxLength)]
        public string Description { get; set; }

        public virtual ICollection<Diploma> Diploma
        {
            get
            {
                return this.diploms;
            }
            set
            {
                this.diploms = value;
            }
        }

        public virtual ICollection<User> Students
        {
            get
            {
                return students;
            }
            set
            {
                students = value;
            }
        }

        public virtual ICollection<Semester> Semesters
        {
            get
            {
                return semesters;
            }
            set
            {
                semesters = value;
            }
        }
    }
}