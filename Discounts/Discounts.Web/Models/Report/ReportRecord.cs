using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discounts.Web.Models.Report
{
    public class ReportRecord
    {
        public string ActionName { get; set; }
        public string UserName { get; set; }
        public string PartnerName { get; set; }
        public string PartnerTypeName { get; set; }
        public decimal OriginalValue { get; set; }
        public decimal ActionValue { get; set; }
    }
}
