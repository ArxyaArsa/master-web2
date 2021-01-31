using AutoMapper;
using Discounts.DataLayer;
using Discounts.DataLayer.Models;
using Discounts.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Discounts.Services.Services
{
    public class UsedActionService : IUsedActionService
    {
        #region dependencies & constructor
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UsedActionService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        public IEnumerable<UsedAction> GetUsedActions()
        {
            return _context.UsedAction.AsEnumerable();
        }

        public UsedAction GetUsedAction(int? id)
        {
            return _context.UsedAction.Find(id);
        }

        public IEnumerable<UsedAction> GetUsedActions(int? userId, int? partnerId, int? actionId)
        {
            return _context.UsedAction.Where(x => x.PartnerId == partnerId && x.UserId == userId && x.ActionId == actionId);
        }

        public bool UsedActionExists(int? userId, int? partnerId, int? actionId)
        {
            return _context.UsedAction.Any(x => x.PartnerId == partnerId && x.UserId == userId && x.ActionId == actionId);
        }

        public UsedAction Create(UsedAction e)
        {
            var ret = _context.UsedAction.Add(e).Entity;
            _context.SaveChanges();
            return ret;
        }

        public UsedAction Update(UsedAction e)
        {
            var ret = _context.UsedAction.Update(e).Entity;
            _context.SaveChanges();
            return ret;
        }

        public void Delete(int? id)
        {
            var map = _context.UsedAction.Find(id);

            if (map == null)
                return;

            _context.UsedAction.Remove(map);
            _context.SaveChanges();
        }
    }
}
