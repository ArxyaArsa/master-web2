using Discounts.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Discounts.Web.Areas.Partner.Controllers
{
    [DiscountsAuthorize(RolesArray = new[] { WebConstants.AdminRole, WebConstants.PartnerRole })]
    [Area("Partner")]
    public class PartnerBaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewData["Area"] = "Partner";

            base.OnActionExecuting(context);
        }
    }
}
