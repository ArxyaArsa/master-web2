using System;
using System.ComponentModel;

namespace Discounts.Web.Areas.Partner.Models
{
    public class ActionViewModel
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

        [DisplayName("Created Date")]
        public DateTime? CreatedDate { get; set; }

        [DisplayName("Is Canceled")]
        public bool IsCanceled { get; set; }

        [DisplayName("Cancel Date")]
        public DateTime? CancelDate { get; set; }

        [DisplayName("Cancel Reason")]
        public string CancelReason { get; set; }
    }
}
