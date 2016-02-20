namespace UniversityStudentSystem.Web.Models.Resources
{
    using Data.Models;
    using UniversityStudentSystem.Web.Infrastructure.Mapping;

    public class ResourceViewModel : IMapFrom<Resource>
    {
        public string Name { get; set; }

        public string Path { get; set; }
    }
}