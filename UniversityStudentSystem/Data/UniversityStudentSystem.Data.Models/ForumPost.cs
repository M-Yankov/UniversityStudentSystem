namespace UniversityStudentSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Common;
    using CommonModels;
    using CustomAttributes;

    public class ForumPost : BaseModel<int>
    {
        private ICollection<Comment> comments;

        public ForumPost()
        {
            this.comments = new HashSet<Comment>();
        }

        [Required]
        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.ContentMaxLength)]
        public string Title { get; set; }

        [Required]
        [DataRange(ModelConstants.MinDate, ModelConstants.MaxDate)]
        public DateTime DatePosted { get; set; }

        [Required]
        [MinLength(ModelConstants.NameMinLength)]
        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Categoty { get; set; }

        public ICollection<Comment> Comments
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
    }
}
