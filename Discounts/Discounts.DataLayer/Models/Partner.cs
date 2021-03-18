using System;
using System.Collections.Generic;
using System.Text;

namespace Discounts.DataLayer.Models
{
    public class Partner
    {
        public int Id { get; set; }
        public int PartnerTypeId { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual PartnerType PartnerType { get; set; }

        public virtual ICollection<PartnerActionMap> PartnerActionMaps { get; set; }
        public virtual ICollection<UsedAction> UsedActions { get; set; }
        public virtual ICollection<DiscountsUser> Users { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
