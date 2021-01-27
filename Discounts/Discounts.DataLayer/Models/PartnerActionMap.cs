using System;
using System.Collections.Generic;
using System.Text;

namespace Discounts.DataLayer.Models
{
    public class PartnerActionMap
    {
        public int Id { get; set; }
        public int PartnerId { get; set; }
        public int ActionId { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Partner Partner { get; set; }
        public virtual Action Action { get; set; }
    }
}
