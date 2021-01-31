using Discounts.DataLayer.Models;
using Discounts.Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discounts.Services.Interfaces
{
    public interface IPartnerService
    {
        IEnumerable<Partner> GetPartners();
        bool Exists(int? id);
        Partner Create(Partner partner);
        Partner Update(Partner partner);
        void Delete(int? id);
    }
}
