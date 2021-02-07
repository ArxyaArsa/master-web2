using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Discounts.Web.Areas.User.Models
{
    public class ViewByActionModel
    {
        public int Id { get; set; }

        [DisplayName("Discount Action")]
        public string Name { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("$ value")]
        public decimal? CashValue { get; set; }

        [DisplayName("% value")]
        public decimal? PercentValue { get; set; }

        [DisplayName("Start Date")]
        public DateTime? StartDate { get; set; }

        [DisplayName("End Date")]
        public DateTime? EndDate { get; set; }

        [DisplayName("Finished")]
        public bool IsFinished { get; set; }

        [DisplayName("Is Used")]
        public bool IsUsed { get; set; }

        public DateTime? CreatedDate { get; set; }
        public bool IsCanceled { get; set; }
        public DateTime? CancelDate { get; set; }
        public string CancelReason { get; set; }
    }
}
