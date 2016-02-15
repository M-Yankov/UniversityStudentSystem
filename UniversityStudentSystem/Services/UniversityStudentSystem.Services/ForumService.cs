namespace UniversityStudentSystem.Services
{
    using System;
    using System.Linq;
    using Data.Models;
    using Data.Repositories;
    using UniversityStudentSystem.Services.Contracts;

    public class ForumService : IForumService
    {
        private IRepository<ForumPost> forumRepository;
        private IRepository<Comment> commentRepository;


        public ForumService(IRepository<ForumPost> forumRepo, IRepository<Comment> commentRepo)
        {
            this.forumRepository = forumRepo;
            this.commentRepository = commentRepo;
        }

        public IQueryable<ForumPost> GetAll()
        {
            return this.forumRepository.All();
        }

        public void PostComment(string content, int postId, string userId)
        {
            Comment comment = new Comment()
            {
                Content = content,
                ForumPostId = postId,
                UserId = userId
            };

            this.commentRepository.Add(comment);
            this.commentRepository.Save();
        }
    }
}
