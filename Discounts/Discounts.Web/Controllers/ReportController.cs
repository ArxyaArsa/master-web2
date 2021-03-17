using Discounts.Web.Factories;
using Discounts.Web.Helpers;
using Discounts.Web.Models.Report;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discounts.Web.Controllers
{
    [DiscountsAuthorize(RolesArray = new[] { WebConstants.AdminRole, WebConstants.PartnerRole, WebConstants.UserRole })]
    public class ReportController : Controller
    {
        #region dependencies and constructor
        private readonly UserFactory _userFactory;
        private readonly ReportFactory _reportFactory;

        public ReportController(
            UserFactory userFactory,
            ReportFactory reportFactory
        )
        {
            _userFactory = userFactory;
            _reportFactory = reportFactory;
        }
        #endregion

        public IActionResult Index()
        {
            var model = new ReportPageModel();

            if (User.IsInRole(WebConstants.UserRole))
            {
                var user = _userFactory.GetUser(User.Identity.Name);

                if (user == null)
                    throw new ApplicationException("No record of such user");

                model.UserMode = true;
                model.UserIds = new List<int>() { user.Id };
            }

            if (User.IsInRole(WebConstants.PartnerRole))
            {
                var user = _userFactory.GetUser(User.Identity.Name);

                if (user == null)
                    throw new ApplicationException("No record of such user");

                if (user.PartnerId == null)
                    throw new ApplicationException("The current user has the permissions of a Partner, but isn't tied to any");

                model.PartnerMode = true;
                model.PartnerIds = new List<int>() { user.PartnerId.Value };
            }

            return View("Index", model);
        }

        public IActionResult Build([Bind] ReportFilterModel filter)
        {
            var data = _reportFactory.GenerateReportData(filter);

            // now export to excel (and save everything to db)

            return Index();
        }
    }
}
