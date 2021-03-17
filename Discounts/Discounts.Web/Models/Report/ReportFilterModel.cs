using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discounts.Web.Models.Report
{
    public class ReportFilterModel
    {
        public bool GroupByAction { get; set; }
        public bool GroupByUser { get; set; }
        public bool GroupByPartner { get; set; }
        public bool GroupByPartnerType { get; set; }
        public List<int> PartnerIds { get; set; }
        public List<int> ActionIds { get; set; }
        public List<int> UserIds { get; set; }
        public List<int> PartnerTypeIds { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
