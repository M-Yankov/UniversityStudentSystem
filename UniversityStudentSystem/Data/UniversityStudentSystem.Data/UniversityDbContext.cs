namespace UniversityStudentSystem.Data
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public class UniversityDbContext : IdentityDbContext<User>, IUniversityDbContext
    {
        public UniversityDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public IDbSet<Answer> Answers { get; set; }

        public IDbSet<Candidate> Candidates { get; set; }

        public IDbSet<Category> Categories { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<Course> Courses { get; set; }

        public IDbSet<CourseTask> Tasks { get; set; }

        public IDbSet<Diploma> Diploms { get; set; }

        public IDbSet<Document> Documents { get; set; }

        public IDbSet<ForumPost> ForumPosts { get; set; }

        public IDbSet<Mark> Marks { get; set; }

        public IDbSet<Message> Messages { get; set; }

        public IDbSet<News> News { get; set; }

        public IDbSet<Question> Questions { get; set; }

        public IDbSet<Semester> Semesters { get; set; }

        public IDbSet<Specialty> Speciallties { get; set; }

        public IDbSet<Test> Tests { get; set; }

        public static UniversityDbContext Create()
        {
            return new UniversityDbContext();
        }
    }
}
