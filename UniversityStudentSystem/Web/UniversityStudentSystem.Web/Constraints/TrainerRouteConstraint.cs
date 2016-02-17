using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using UniversityStudentSystem.Common;

namespace UniversityStudentSystem.Web.Constraints
{
    public class TrainerRouteConstraint : IRouteConstraint
    {
        public bool Match(
            HttpContextBase httpContext,
            Route route,
            string parameterName,
            RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            return (httpContext.User.IsInRole(RoleConstants.Admin) ||
                    httpContext.User.IsInRole(RoleConstants.Trainer));
        }
    }
}