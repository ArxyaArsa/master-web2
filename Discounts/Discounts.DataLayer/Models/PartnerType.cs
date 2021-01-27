using System;
using System.Collections.Generic;
using System.Text;

namespace Discounts.DataLayer.Models
{
    public class PartnerType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Partner> Partners { get; set; }
    }
}
