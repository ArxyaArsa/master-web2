using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discounts.Web.Areas.Partner.Models
{
    public class CustomErrorViewModel
    {
        public string HeaderMessage { get; set; }
        public string Message { get; set; }
        public Dictionary<string, string> ReturnUrls { get; set; }
    }
}
