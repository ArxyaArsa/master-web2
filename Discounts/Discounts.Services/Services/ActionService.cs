using AutoMapper;
using Discounts.DataLayer;
using Discounts.DataLayer.Models;
using Discounts.Services.Helpers;
using Discounts.Services.Interfaces;
using Discounts.Services.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discounts.Services.Services
{
    public class ActionService : IActionService
    {
        #region dependencies & constructor
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ActionService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        public IEnumerable<DiscountAction> GetActions()
        {
            return _context.DiscountAction.AsEnumerable();
        }

        public DiscountAction GetAction(int? id)
        {
            return _context.DiscountAction.Find(id);
        }

        public DiscountAction Create(DiscountAction model)
        {
            var ret = _context.DiscountAction.Add(model).Entity;
            _context.SaveChanges();
            return ret;
        }

        public DiscountAction Update(DiscountAction model)
        {
            var ret = _context.DiscountAction.Update(model).Entity;
            _context.SaveChanges();
            return ret;
        }

        public void Delete(int? id)
        {
            var action = _context.DiscountAction.Find(id);

            if (action == null)
                return;

            _context.DiscountAction.Remove(action);
            _context.SaveChanges();
        }
    }
}
