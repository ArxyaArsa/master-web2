using System;
using System.Collections.Generic;
using System.Text;

namespace Discounts.Services.Models
{
    public class ActionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? CashValue { get; set; }
        public decimal? PercentValue { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCanceled { get; set; }
        public DateTime? CancelDate { get; set; }
        public string CancelReason { get; set; }
    }
}
