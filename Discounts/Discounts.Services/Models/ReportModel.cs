using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Discounts.Services.Models
{
    public class ReportModel
    {
        public int Id { get; set; }

        [DisplayName("Creator UserName")]
        public string DiscountsUserName { get; set; }
        public int DiscountsUserId { get; set; }

        [DisplayName("Partner Name")]
        public string PartnerName { get; set; }
        public int? PartnerId { get; set; }

        [DisplayName("Report Name")]
        public string Name { get; set; }

        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }

        public string PathToFile { get; set; }
        public string FilterJson { get; set; }
    }
}
