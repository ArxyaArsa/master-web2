﻿using System.Collections.Generic;

namespace Discounts.Web.Areas.User.Models
{
    public class CustomErrorViewModel
    {
        public string HeaderMessage { get; set; }
        public string Message { get; set; }
        public Dictionary<string, string> ReturnUrls { get; set; }
    }
}
