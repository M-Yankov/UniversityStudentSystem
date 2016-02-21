namespace UniversityStudentSystem.Web.Models.Solutions
{
    using System;
    using UniversityStudentSystem.Data.Models;
    using UniversityStudentSystem.Web.Infrastructure.Mapping;
    using Users;

    public class SolutionViewModel : IMapFrom<Solution>
    {
        public DateTime CreatedOn { get; set; }

        public string Path { get; set; }

        public UserViewModel User { get; set; }
    }
}