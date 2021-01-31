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

        public string PartnerName { get; set; }
        public string ActionName { get; set; }
    }
}
