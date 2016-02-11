namespace UniversityStudentSystem.Web
{
    using System.Web.Mvc;

    public class ViewEnginesConfig
    {
        public static void Register()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }
    }
}