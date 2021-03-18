using Discounts.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discounts.Web.Models.Report
{
    public class ReportPageModel : ReportFilterModel
    {
        public bool UserMode { get; set; }
        public bool PartnerMode { get; set; }

        public IEnumerable<ReportModel> MyReports { get; set; }
        public IEnumerable<ReportModel> PartnerReports { get; set; }
        public IEnumerable<ReportModel> AdminReports { get; set; }
    }
}
