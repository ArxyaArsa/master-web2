using Discounts.DataLayer.Models;
using Discounts.Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discounts.Services.Interfaces
{
    public interface IPartnerTypeService
    {
        IEnumerable<PartnerType> GetPartnerTypes();
        PartnerType GetPartnerType(int? id);
        PartnerType Create(PartnerType model);
        PartnerType Update(PartnerType model);
        void Delete(int? id);
    }
}
