namespace UniversityStudentSystem.Web.Models.ForumPosts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using UniversityStudentSystem.Common;

    public class ForumInputModel
    {
        [Required]
        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.ContentMaxLength)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [MinLength(ModelConstants.NameMinLength)]
        public string Content { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}