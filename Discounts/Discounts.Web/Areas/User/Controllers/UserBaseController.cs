using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discounts.Web.Areas.User.Controllers
{
    [Authorize(Roles = "admin,user")]
    [Area("User")]
    public class UserBaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewData["Area"] = "User";

            base.OnActionExecuting(context);
        }
    }
}
