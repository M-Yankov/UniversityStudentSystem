namespace UniversityStudentSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Common;
    using CommonModels;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser, IIdentifiableEntity<string>, IAuditInfo, IDeletableEntity
    {
        private ICollection<Candidate> candidatures;
        private ICollection<Diploma> diploms;
        private ICollection<Specialty> specialties;
        private ICollection<Comment> comments;
        private ICollection<ForumPost> forumPosts;
        private ICollection<Course> courses;
        private ICollection<Mark> marks;
        private ICollection<Solution> solutions;

        public User()
        {
            this.candidatures = new HashSet<Candidate>();
            this.diploms = new HashSet<Diploma>();
            this.specialties = new HashSet<Specialty>();
            this.comments = new HashSet<Comment>();
            this.forumPosts = new HashSet<ForumPost>();
            this.marks = new HashSet<Mark>();
            this.courses = new HashSet<Course>();
            this.solutions = new HashSet<Solution>();
        }

        [Required]
        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.NameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.NameMaxLength)]
        public string LastName { get; set; }

        [Required]
        public Genre Genre { get; set; }

        [Required]
        [Range(ModelConstants.MinAge, ModelConstants.MaxAge)]
        public int Age { get; set; }

        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.NameMaxLength)]
        [RegularExpression(ModelConstants.FacebookProfileRegularExpression)]
        public string FacebookAccount { get; set; }

        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.NameMaxLength)]
        public string SkypeName { get; set; }

        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.NameMaxLength)]
        public string LinkedInProfile { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [Range(ModelConstants.FacultyStartNumber, ModelConstants.FacultyEndNumber)]
        public long FacultyNumber { get; set; }

        [Required]
        public DateTime DateRegistered { get; set; }

        public Status Status { get; set; }

        public bool IsGroupManager { get; set; }

        [MaxLength(ModelConstants.DescriptionMaxLength)]
        public string AboutMe { get; set; }

        [MaxLength(ModelConstants.ContentMaxLength)]
        public string AvaratUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public ICollection<Candidate> Candidatures
        {
            get
            {
                return this.candidatures;
            }
            set
            {
                this.candidatures = value;
            }
        }

        public virtual ICollection<Diploma> Diploms
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

        public virtual ICollection<Specialty> Specialties
        {
            get
            {
                return this.specialties;
            }
            set
            {
                this.specialties = value;
            }
        }

        public virtual ICollection<Comment> Comments
        {
            get
            {
                return this.comments;
            }
            set
            {
                this.comments = value;
            }
        }

        public ICollection<Course> Courses
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

        public virtual ICollection<ForumPost> ForumPosts
        {
            get
            {
                return this.forumPosts;
            }
            set
            {
                this.forumPosts = value;
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

        

        public ICollection<Solution> Solutions
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

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}