using Discounts.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Discounts.Web.Areas.Admin.Controllers
{
    [DiscountsAuthorize(RolesArray = new[] { WebConstants.AdminRole })]
    [Area ("Admin")]
    public class AdminBaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewData["Area"] = "Admin";

            base.OnActionExecuting(context);
        }
    }
}
