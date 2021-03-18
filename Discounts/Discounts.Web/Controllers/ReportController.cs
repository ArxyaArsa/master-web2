using Discounts.Web.Factories;
using Discounts.Web.Helpers;
using Discounts.Web.Models.Report;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

        public static string ExcelContentType { get { return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; } }

        public IActionResult Index()
        {
            var model = new ReportPageModel();

            var user = _userFactory.GetUser(User.Identity.Name);

            if (user == null)
                throw new ApplicationException("No record of such user");

            model.MyReports = _reportFactory.GetReportsForUser(user.Id);

            if (User.IsInRole(WebConstants.UserRole))
            {
                model.UserMode = true;
                model.UserIds = new List<int>() { user.Id };
            }

            if (User.IsInRole(WebConstants.PartnerRole))
            {
                if (user.PartnerId == null)
                    throw new ApplicationException("The current user has the permissions of a Partner, but isn't tied to any");

                model.PartnerMode = true;
                model.PartnerIds = new List<int>() { user.PartnerId.Value };

                model.PartnerReports = _reportFactory.GetPartnerReports(user.PartnerId);
            }

            if (User.IsInRole(WebConstants.AdminRole))
            {
                model.AdminReports = _reportFactory.GetAllReports();
            }

            return View("Index", model);
        }

        public IActionResult Build([Bind] ReportFilterModel filter)
        {
            var user = _userFactory.GetUser(User.Identity.Name);

            if (user == null)
                throw new ApplicationException("No record of such user");

            int userId = user.Id;

            int? partnerId = null;
            if (User.IsInRole(WebConstants.PartnerRole))
            {
                if (user == null)
                    throw new ApplicationException("No record of such user");

                if (user.PartnerId == null)
                    throw new ApplicationException("The current user has the permissions of a Partner, but isn't tied to any");

                partnerId = user.PartnerId;
            }

            var data = _reportFactory.GenerateReportData(filter);

            // now export to excel (and save everything to db !!!)
            var reportPath = ReportHelper.Export(data, filter);

            _reportFactory.CreateReport(new Services.Models.ReportModel()
            {
                Name = Path.GetFileNameWithoutExtension(reportPath),
                CreatedDate = DateTime.UtcNow,
                DiscountsUserId = userId,
                FilterJson = JsonConvert.SerializeObject(filter),
                PartnerId = partnerId,
                PathToFile = reportPath
            });

            return Index();
        }

        public IActionResult Download(int id)
        {
            // check if can download

            var report = _reportFactory.GetReport(id);

            var phPath = Path.GetFullPath(report.PathToFile);

            return new PhysicalFileResult(phPath, ExcelContentType);
        }
    }
}
