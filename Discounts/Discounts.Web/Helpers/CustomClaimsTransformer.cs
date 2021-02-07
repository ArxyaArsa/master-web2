using Discounts.DataLayer;
using Discounts.Services.Interfaces;
using Discounts.Web.Factories;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Discounts.Web.Helpers
{
    public class CustomClaimsTransformer : IClaimsTransformation
    {
        private readonly ApplicationDbContext _context;

        public CustomClaimsTransformer(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var ci = (ClaimsIdentity)principal.Identity;

            var roles = _context.UserRoles.Where(x => x.User.UserName == ci.Name).Select(x => x.Role.Name).ToList();
            if (roles != null && roles.Count > 0)
            {
                ci.AddClaims(roles.Select(x => new Claim(ci.RoleClaimType, x)));
            }

            var partnerId = _context.Users.FirstOrDefault(x => x.UserName == ci.Name)?.PartnerId;

            if (partnerId != null)
            {
                ci.AddClaim(new Claim(WebConstants.PartnerClaimType, partnerId.Value.ToString()));
            }

            return Task.FromResult(principal);
        }
    }
}
