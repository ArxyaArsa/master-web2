using Discounts.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Discounts.Web.Areas.User.Controllers
{
    [DiscountsAuthorize(RolesArray = new[] { WebConstants.AdminRole, WebConstants.UserRole })]
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
