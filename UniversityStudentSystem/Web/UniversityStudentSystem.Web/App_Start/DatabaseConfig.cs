namespace UniversityStudentSystem.Web
{
    using System.Data.Entity;
    using UniversityStudentSystem.Data;
    using UniversityStudentSystem.Data.Migrations;

    public class DatabaseConfig
    {
        public static void Initalize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<UniversityDbContext, Configuration>());
            UniversityDbContext.Create().Database.Initialize(true);
        }
    }
}