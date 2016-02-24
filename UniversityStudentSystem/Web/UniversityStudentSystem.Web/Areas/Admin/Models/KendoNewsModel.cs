namespace UniversityStudentSystem.Web.Areas.Admin.Models
{
    using System;
    using UniversityStudentSystem.Data.Models;
    using UniversityStudentSystem.Web.Infrastructure.Mapping;

    public class KendoNewsModel : IMapFrom<News>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string PhotoPath { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}