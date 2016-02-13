namespace UniversityStudentSystem.Web.Models.ForumPosts
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Comments;
    using UniversityStudentSystem.Data.Models;
    using UniversityStudentSystem.Web.Infrastructure.Mapping;

    public class ForumPostViewModel : IMapFrom<ForumPost>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Username { get; set; }

        public string Category { get; set; }

        public DateTime CreatedOn { get; set; }

        public IList<CommentViewModel> Comments { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<ForumPost, ForumPostViewModel>()
                    .ForMember(c => c.Username, o => o.MapFrom(f => f.User.UserName))
                    .ForMember(c => c.Category, o => o.MapFrom(f => f.Categoty.Name));
        }
    }
}