using System;
using System.Collections.Generic;
using System.Text;

namespace Discounts.Services.Models
{
    public class PartnerModel
    {
        public int Id { get; set; }
        public int PartnerTypeId { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string PartnerTypeName { get; set; }
    }
}
