namespace UniversityStudentSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class Course
    {
        private ICollection<User> trainers;
        private ICollection<Test> tests;
        private ICollection<CourseTask> tasks;

        public Course()
        {
            this.trainers = new HashSet<User>();
            this.tests = new HashSet<Test>();
            this.tasks = new HashSet<CourseTask>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.NameMaxLength)]
        public string Name { get; set; }

        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.DescriptionMaxLength)]
        public string Description { get; set; }

        public int SemesterId { get; set; }

        public virtual Semester Semester { get; set; }

        public ICollection<CourseTask> Tasks
        {
            get
            {
                return this.tasks;
            }
            set
            {
                this.tasks = value;
            }
        }
        
        public ICollection<Test> Tests
        {
            get
            {
                return this.tests;
            }
            set
            {
                this.tests = value;
            }
        }

        public virtual ICollection<User> Trainers
        {
            get
            {
                return this.trainers;
            }
            set
            {
                this.trainers = value;
            }
        }
    }
}