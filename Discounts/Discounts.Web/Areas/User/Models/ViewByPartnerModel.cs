using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Discounts.Web.Areas.User.Models
{
    public class ViewByPartnerModel
    {
        public int Id { get; set; }

        [DisplayName("Partner")]
        public string Name { get; set; }

        [DisplayName("# of Discount Actions")]
        public int ActionCount { get; set; }

        [DisplayName("# of Used Actions")]
        public int UsedActionCount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PartnerTypeId { get; set; }

        [DisplayName("Category")]
        public string PartnerTypeName { get; set; }
    }
}
