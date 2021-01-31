using Discounts.DataLayer.Models;
using Discounts.Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discounts.Services.Interfaces
{
    public interface IUsedActionService
    {
        IEnumerable<UsedAction> GetUsedActions();
        UsedAction GetUsedAction(int? id);
        IEnumerable<UsedAction> GetUsedActions(int? userId, int? partnerId, int? actionId);
        bool UsedActionExists(int? userId, int? partnerId, int? actionId);
        UsedAction Create(UsedAction entry);
        UsedAction Update(UsedAction entry);
        void Delete(int? id);
    }
}
