using System;
using System.Collections.Generic;
using System.Text;

namespace Discounts.DataLayer.Models
{
    public class Report
    {
        public int Id { get; set; }
        public int DiscountsUserId { get; set; }
        public int? PartnerId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string PathToFile { get; set; }
        public string FilterJson { get; set; }

        public virtual DiscountsUser DiscountsUser { get; set; }
        public virtual Partner Partner { get; set; }
    }
}
