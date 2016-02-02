namespace UniversityStudentSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

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
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(15000)]
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