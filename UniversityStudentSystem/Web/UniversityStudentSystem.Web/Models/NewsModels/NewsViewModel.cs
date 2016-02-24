namespace UniversityStudentSystem.Web.Models.NewsModels
{
    using System;
    using AutoMapper;
    using Common;
    using Common.Extensions;
    using Data.Models;
    using UniversityStudentSystem.Web.Infrastructure.Mapping;

    public class NewsViewModel : IMapFrom<News>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string TitleUI
        {
            get
            {
                if (this.Title == null)
                {
                    return string.Empty;
                }

                return this.Title.ToUrl();
            }
        }

        public string Content { get; set; }

        public string PhotoPath { get; set; }

        public string ShortContent
        {
            get
            {
                int length = this.Content.Length > WebConstants.NewsShortContentLength ?
                    WebConstants.NewsShortContentLength :
                    this.Content.Length - 1;

                int lastSpace = this.Content.LastIndexOf(' ', length);

                string shortContent = this.Content.Substring(0, lastSpace);
                return $"{shortContent} ...";
            }
        }

        public DateTime CreatedOn { get; set; }

        public string CreatedOnUI
        {
            get
            {
                return this.CreatedOn.DateTimeAgo();
            }
        }
    }
}