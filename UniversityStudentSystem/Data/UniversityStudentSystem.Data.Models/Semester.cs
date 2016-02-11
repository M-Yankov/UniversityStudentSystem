namespace UniversityStudentSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Common;
    using CommonModels;
    using CustomAttributes;

    public class Semester: BaseModel<int>
    {
        private ICollection<Course> courses;

        public Semester()
        {
            this.courses = new HashSet<Course>();
        }

        [Required]
        [MaxLength(ModelConstants.NameMaxLength)]
        public string Name { get; set; }
        
        [Required]
        [Range(0, double.MaxValue)]
        public int Fee { get; set; }

        [Required]
        [DataRange(ModelConstants.MinDate, ModelConstants.MaxDate)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataRange(ModelConstants.MinDate, ModelConstants.MaxDate)]
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