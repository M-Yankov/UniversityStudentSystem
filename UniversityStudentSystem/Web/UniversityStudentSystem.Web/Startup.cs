[assembly: Microsoft.Owin.OwinStartupAttribute(typeof(UniversityStudentSystem.Web.Startup))]

namespace UniversityStudentSystem.Web
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
