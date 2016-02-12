﻿namespace UniversityStudentSystem.Web.Controllers
{
    using System.Web.Mvc;
    using AutoMapper;
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
    }
}