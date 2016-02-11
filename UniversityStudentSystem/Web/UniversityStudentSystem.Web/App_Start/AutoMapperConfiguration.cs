namespace UniversityStudentSystem.Web
{
    using System.Reflection;
    using Infrastructure.Mapping;

    public class AutoMapperConfiguration
    {
        public static void Register()
        {
            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(Assembly.GetExecutingAssembly());
        }
    }
}