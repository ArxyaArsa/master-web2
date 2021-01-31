using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discounts.DataLayer.Models
{
    public class DiscountsUserRole : IdentityUserRole<int>
    {
        public virtual DiscountsUser User { get; set; }
        public virtual DiscountsRole Role { get; set; }
    }
}
