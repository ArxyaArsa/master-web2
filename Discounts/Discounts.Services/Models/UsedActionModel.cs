using System;
using System.Collections.Generic;
using System.Text;

namespace Discounts.Services.Models
{
    public class UsedActionModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ActionId { get; set; }
        public int PartnerId { get; set; }
        public decimal ActionValue { get; set; }
        
        public UserModel User { get; set; }
        public PartnerModel Partner { get; set; }
        public ActionModel Action { get; set; }
    }
}
