using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Discounts.Web.Areas.User.Models
{
    public class ViewByCategoryModel
    {
        public int Id { get; set; }

        [DisplayName("Category")]
        public string Name { get; set; }

        [DisplayName("# of Partners")]
        public int PartnerCount { get; set; }

        [DisplayName("# of Discount Actions")]
        public int ActionCount { get; set; }
    }
}
