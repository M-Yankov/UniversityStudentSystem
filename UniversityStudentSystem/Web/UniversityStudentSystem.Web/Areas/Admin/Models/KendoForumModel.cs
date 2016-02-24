namespace UniversityStudentSystem.Web.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Common.Extensions;
    using UniversityStudentSystem.Data.Models;
    using UniversityStudentSystem.Web.Infrastructure.Mapping;

    public class KendoForumModel : IMapFrom<ForumPost>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Username { get; set; }

        public string Category { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<ForumPost, KendoForumModel>()
                    .ForMember(c => c.Username, o => o.MapFrom(f => f.User.UserName))
                    .ForMember(c => c.Category, o => o.MapFrom(f => f.Categoty.Name));
        }
    }
}