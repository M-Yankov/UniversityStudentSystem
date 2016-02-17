using System.Web;

namespace UniversityStudentSystem.Web.HelperProviders
{
    public class NewsImageUploader
    {
        public string Save(HttpServerUtilityBase server, HttpPostedFileBase file)
        {
            string path = "/Images/" + file.FileName;
            string fullPath = server.MapPath(path);
            file.SaveAs(fullPath);

            return path;
        }
    }
}