namespace UniversityStudentSystem.Web.HelperProviders
{
    using System.IO;
    using System.Web;
    using System.Web.Mvc;

    public class UserManagement
    {
        private HttpServerUtilityBase server;

        public UserManagement(HttpServerUtilityBase serverBase)
        {
            this.server = serverBase;
        }

        public void EnsureFolder(string userId)
        {
            string path = this.server.MapPath($"~/App_Data/Users/{ userId }");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public string GetCurrentUserDirecotry(string userId)
        {
            return this.server.MapPath($"~/App_Data/Users/{ userId }");
        }
    }
}