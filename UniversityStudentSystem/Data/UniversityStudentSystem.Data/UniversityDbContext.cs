namespace UniversityStudentSystem.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using Models.CommonModels;

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

        public IDbSet<BugReport> BugReports { get; set; }

        public IDbSet<Resource> Resources { get; set; }

        public IDbSet<Solution> Solutions { get; set; }

        public IDbSet<TestResult> TestResults { get; set; }

        public static UniversityDbContext Create()
        {
            return new UniversityDbContext();
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            // EF - should die some day !
            try
            {
                return base.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
