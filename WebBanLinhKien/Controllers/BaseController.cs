using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebBanLinhKien.Common;

namespace WebBanLinhKien.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (Login)Session[CommomConstants.User_Session];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "LoginCli", action = "Index" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}