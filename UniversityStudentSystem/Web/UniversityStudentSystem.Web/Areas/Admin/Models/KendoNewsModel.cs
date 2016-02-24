namespace UniversityStudentSystem.Web.Areas.Admin.Models
{
    using UniversityStudentSystem.Web.Infrastructure.Mapping;
    using UniversityStudentSystem.Data.Models;
    using System;

    public class KendoNewsModel : IMapFrom<News>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string PhotoPath { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}