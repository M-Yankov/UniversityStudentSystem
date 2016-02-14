namespace UniversityStudentSystem.Web.HelperProviders
{
    using System.Collections.Generic;

    public class SaveImageResult
    {
        public bool HasSucceed { get; set; }

        public string Error { get; set; }

        public string Path { get; set; }
    }
}