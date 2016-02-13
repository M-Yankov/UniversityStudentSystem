namespace UniversityStudentSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Common;
    using CommonModels;

    public class Comment : BaseModel<int>
    {
        [Required]
        [MinLength(ModelConstants.NameMinLength)]
        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int ForumPostId { get; set; }

        public virtual ForumPost ForumPost { get; set; }
    }
}
