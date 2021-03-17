﻿using System;
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
        public decimal OriginalValue { get; set; }
        public DateTime? DateCreated { get; set; }

        public string UserName { get; set; }
        public string PartnerName { get; set; }
        public string ActionName { get; set; }
    }
}
