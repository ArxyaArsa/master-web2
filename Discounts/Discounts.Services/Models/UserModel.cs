using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Discounts.Services.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public int? PartnerId { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool EmailConfirmed { get; set; }
        public int AccessFailedCount { get; set; }
        public string ConcurrencyStamp { get; set; }

        public string PartnerName { get; set; }
        public IList<string> Roles { get; set; }
    }
}
