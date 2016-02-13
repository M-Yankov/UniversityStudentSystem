namespace UniversityStudentSystem.Web.Models.NewsModels
{
    using System;
    using AutoMapper;
    using Common;
    using Data.Models;
    using UniversityStudentSystem.Web.Infrastructure.Mapping;

    public class NewsViewModel : IMapFrom<News>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ShortContent
        {
            get
            {
                int lastSpace = this.Content.LastIndexOf(' ', WebConstants.NewsShortContentLength);
                string shortContent = this.Content.Substring(0, lastSpace);
                return $"{shortContent} ...";
            }
        }

        public DateTime CreatedOn { get; set; }
    }
}