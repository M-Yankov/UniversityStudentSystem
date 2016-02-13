namespace UniversityStudentSystem.Web.Models.Home
{
    using System.Collections.Generic;
    using UniversityStudentSystem.Web.Models.NewsModels;

    public class HomeViewModel
    {
        public IList<NewsViewModel> News { get; set; }
    }
}