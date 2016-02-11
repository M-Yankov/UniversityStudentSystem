namespace UniversityStudentSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Common;
    using CommonModels;
    using CustomAttributes;

    public class Comment : BaseModel<int>
    {
        [Required]
        [DataRange(ModelConstants.MinDate, ModelConstants.MaxDate)]
        public DateTime DatePosted { get; set; }

        [Required]
        [MinLength(ModelConstants.NameMinLength)]
        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int ForumPostId { get; set; }

        public virtual ForumPost ForumPost { get; set; }
    }
}
