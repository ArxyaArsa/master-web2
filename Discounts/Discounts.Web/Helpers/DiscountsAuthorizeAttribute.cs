using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discounts.Web.Helpers
{
    public class DiscountsAuthorizeAttribute : AuthorizeAttribute
    {
        private string[] _rolesArray;
        public string[] RolesArray
        {
            get { return _rolesArray; }
            set
            {
                if (value != null && value.Length > 0)
                    Roles = value.Aggregate((c, n) => c + "," + n);
                else
                    Roles = null;
                _rolesArray = value;
            }
        }

        public DiscountsAuthorizeAttribute(string [] RolesArray) : base()
        {
            this.RolesArray = RolesArray;
        }

        public DiscountsAuthorizeAttribute() : base()
        {
        }
    }
}
