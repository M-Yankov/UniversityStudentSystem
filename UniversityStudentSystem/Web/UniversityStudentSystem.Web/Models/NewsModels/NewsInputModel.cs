namespace UniversityStudentSystem.Web.Models.NewsModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using Common;
    using Data.Models;
    using UniversityStudentSystem.Web.Infrastructure.Mapping;

    public class NewsInputModel : IMapTo<News>
    {
        [Required]
        [MinLength(ModelConstants.NameMinLength)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [MinLength(ModelConstants.NameMinLength)]
        public string Content { get; set; }

        [MaxLength(ModelConstants.ContentMaxLength)]
        public string PhotoPath { get; set; }

        public HttpPostedFileBase File { get; set; }
    }
}