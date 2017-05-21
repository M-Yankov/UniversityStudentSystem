namespace UniversityStudentSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Common;
    using CommonModels;

    public class Category : BaseModel<int>
    {
        private ICollection<ForumPost> forumPosts;

        public Category()
        {
            this.forumPosts = new HashSet<ForumPost>();
        }

        [Required]
        [Index(IsUnique = true)]
        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.NameMaxLength)]
        public string Name { get; set; }

        public virtual ICollection<ForumPost> ForumPosts
        {
            get
            {
                return forumPosts;
            }

            set
            {
                forumPosts = value;
            }
        }
    }
}