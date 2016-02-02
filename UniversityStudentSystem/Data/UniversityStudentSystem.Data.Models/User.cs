namespace UniversityStudentSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        private ICollection<Diploma> diploms;
        private ICollection<Specialty> specialties;
        private ICollection<Comment> comments;
        private ICollection<ForumPost> forumPosts;
        private ICollection<Mark> marks;

        public User()
        {
            this.diploms = new HashSet<Diploma>();
            this.specialties = new HashSet<Specialty>();
            this.comments = new HashSet<Comment>();
            this.forumPosts = new HashSet<ForumPost>();
            this.marks = new HashSet<Mark>();
        }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public Genre MyProperty { get; set; }

        [Required]
        [Range(3, 100)]
        public int Age { get; set; }

        [MinLength(3)]
        [MaxLength(50)]
        [RegularExpression("((http|https):\\/\\/|)(www\\.|)facebook\\.com\\/[a-zA-Z0-9.]{1,}")]
        public string FacebookAccount { get; set; }

        [MinLength(3)]
        [MaxLength(50)]
        public string SkypeName { get; set; }

        [MinLength(3)]
        [MaxLength(50)]
        public string LinkedInProfile { get; set; }

        [Required]
        [Range(1000, 999999)]
        public long FacultyNumber { get; set; }

        [Required]
        public DateTime DateRegistered { get; set; }

        public bool IsGroupManager { get; set; }

        [MaxLength(15000)]
        public string AboutMe { get; set; }

        [MaxLength(500)]
        public string AvaratUrl { get; set; }

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

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}