namespace UniversityStudentSystem.Web.Controllers
{
    using System.Web.Mvc;
    using AutoMapper;
    using HelperProviders;
    using Microsoft.AspNet.Identity;
    using UniversityStudentSystem.Web.Infrastructure.Mapping;

    public abstract class BaseController : Controller
    {
        protected IMapper Mapper
        {
            get
            {
                return AutoMapperConfig.Configuration.CreateMapper();
            }
        }

        protected string UserId
        {
            get
            {
                if (!this.Request.IsAuthenticated)
                {
                    return null;
                }

                return this.User.Identity.GetUserId();
            }
        }

        protected UserManagement UserManagement
        {
            get
            {
                return new UserManagement(this.Server);
            }
        }
    }
}