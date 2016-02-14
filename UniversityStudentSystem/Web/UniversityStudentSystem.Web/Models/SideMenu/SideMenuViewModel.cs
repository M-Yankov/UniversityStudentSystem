namespace UniversityStudentSystem.Web.Models.SideMenu
{
    using System.Collections.Generic;
    using Specialties;
    using UniversityStudentSystem.Web.Models.Courses;
    using Users;

    public class SideMenuViewModel
    {
        public IList<CourseViewModel> Courses { get; set; }

        public IList<SpecialtyViewModel> Specialties { get; set; }

        public IList<UserViewModel> Trainers { get; set; }
    }
}