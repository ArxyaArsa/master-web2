using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discounts.DataLayer.Models
{
    public class DiscountsRole : IdentityRole<int>
    {
        public virtual ICollection<DiscountsUserRole> UserRoleMaps { get; set; }
    }
}
