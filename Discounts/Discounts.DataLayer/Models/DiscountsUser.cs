using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discounts.DataLayer.Models
{
    public class DiscountsUser : IdentityUser<int>
    {
        public int? PartnerId { get; set; }

        public virtual Partner Partner { get; set; }
        public virtual ICollection<UsedAction> UsedActions { get; set; }
        public virtual ICollection<DiscountsUserRole> UserRoleMaps { get; set; }
        public virtual ICollection<Report> ReportsOfMyCreation { get; set; }
    }
}
