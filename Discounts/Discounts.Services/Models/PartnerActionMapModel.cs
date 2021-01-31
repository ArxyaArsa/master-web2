using System;
using System.Collections.Generic;
using System.Text;

namespace Discounts.Services.Models
{
    public class PartnerActionMapModel
    {
        public int Id { get; set; }
        public int PartnerId { get; set; }
        public int ActionId { get; set; }
        public DateTime? CreatedDate { get; set; }

        public PartnerModel Partner { get; set; }
        public ActionModel Action { get; set; }
    }
}
