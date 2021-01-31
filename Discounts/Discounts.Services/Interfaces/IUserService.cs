using Discounts.DataLayer.Models;
using Discounts.Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discounts.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<DiscountsUser> GetUsers();
        void UpdateUser(DiscountsUser user, IList<string> roles);
        DiscountsUser CreateUser(DiscountsUser user, IList<string> roles);
        IEnumerable<DiscountsRole> GetRoles();
    }
}
