
namespace UniversityStudentSystem.Web.Models.Comments
{
    using System.ComponentModel.DataAnnotations;
    using Common;
    using Infrastructure.Mapping;
    using UniversityStudentSystem.Data.Models;

    public class CommentInputModel : IMapFrom<Comment>
    {
        [Required]
        [MinLength(ModelConstants.NameMinLength)]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}