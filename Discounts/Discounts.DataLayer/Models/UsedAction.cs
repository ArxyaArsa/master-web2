using System;
using System.Collections.Generic;
using System.Text;

namespace Discounts.DataLayer.Models
{
    public class UsedAction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ActionId { get; set; }
        public int PartnerId { get; set; }
        public decimal ActionValue { get; set; }
        public decimal? OriginalValue { get; set; }
        public DateTime? DateCreated { get; set; }

        public virtual DiscountsUser User { get; set; }
        public virtual Partner Partner { get; set; }
        public virtual DiscountAction Action { get; set; }
    }
}
