using Discounts.DataLayer.Models;
using Discounts.Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discounts.Services.Interfaces
{
    public interface IActionService
    {
        IEnumerable<DiscountAction> GetActions();
        DiscountAction GetAction(int? id);
        bool Exists(int? id);
        DiscountAction Create(DiscountAction model);
        DiscountAction Update(DiscountAction model);
        void Delete(int? id);
    }
}
