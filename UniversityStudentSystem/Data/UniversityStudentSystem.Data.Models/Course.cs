namespace UniversityStudentSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common;
    using CommonModels;

    public class Course : BaseModel<int>
    {
        private ICollection<User> trainers;
        private ICollection<Test> tests;
        private ICollection<CourseTask> tasks;
        private ICollection<Resource> resources;
        private ICollection<Solution> solutions;
        private ICollection<Mark> marks;

        public Course()
        {
            this.trainers = new HashSet<User>();
            this.tests = new HashSet<Test>();
            this.tasks = new HashSet<CourseTask>();
            this.resources = new HashSet<Resource>();
            this.solutions = new HashSet<Solution>();
            this.marks = new HashSet<Mark>();
        }

        [Required]
        [Index(IsUnique = true)]
        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.NameMaxLength)]
        public string Name { get; set; }

        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.DescriptionMaxLength)]
        public string Description { get; set; }

        public int SemesterId { get; set; }

        public virtual Semester Semester { get; set; }

        public virtual ICollection<Resource> Resources
        {
            get
            {
                return this.resources;
            }
            set
            {
                this.resources = value;
            }
        }

        public virtual ICollection<CourseTask> Tasks
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

        public virtual ICollection<Test> Tests
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

        public virtual ICollection<Solution> Solutions
        {
            get
            {
                return this.solutions;
            }
            set
            {
                this.solutions = value;
            }
        }

        public virtual ICollection<Mark> Marks
        {
            get
            {
                return this.marks;
            }
            set
            {
                this.marks = value;
            }
        }
    }
}