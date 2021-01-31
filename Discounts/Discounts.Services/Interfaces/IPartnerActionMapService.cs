using Discounts.DataLayer.Models;
using Discounts.Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discounts.Services.Interfaces
{
    public interface IPartnerActionMapService
    {
        IEnumerable<PartnerActionMap> GetPartnerActionMaps();
        PartnerActionMap GetPartnerActionMap(int? id);
        bool PartnerActionMapExists(int? partnerId, int? actionId);
        PartnerActionMap Create(PartnerActionMap partner);
        PartnerActionMap Update(PartnerActionMap partner);
        void Delete(int? id);
    }
}
