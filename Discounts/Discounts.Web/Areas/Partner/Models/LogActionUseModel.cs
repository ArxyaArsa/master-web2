using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Discounts.Web.Areas.Partner.Models
{
    [Serializable]
    public class LogActionUseModel
    {
        [DisplayName("Purchase Amount")]
        public decimal OriginalValue { get; set; }

        [DisplayName("Discount Action")]
        public int? ActionId { get; set; }
        [DisplayName("User")]
        public int? UserId { get; set; }

        [DisplayName("User Code")]
        public string UserCode { get; set; }
    }
}
