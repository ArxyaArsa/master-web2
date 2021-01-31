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
    public class PartnerActionMapService : IPartnerActionMapService
    {
        #region dependencies & constructor
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PartnerActionMapService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        public IEnumerable<PartnerActionMap> GetPartnerActionMaps()
        {
            return _context.PartnerActionMap.AsEnumerable();
        }

        public PartnerActionMap GetPartnerActionMap(int? id)
        {
            return _context.PartnerActionMap.Find(id);
        }

        public bool PartnerActionMapExists(int? partnerId, int? actionId)
        {
            return _context.PartnerActionMap.Any(x => x.PartnerId == partnerId && x.ActionId == actionId);
        }

        public PartnerActionMap Create(PartnerActionMap map)
        {
            var ret = _context.PartnerActionMap.Add(map).Entity;
            _context.SaveChanges();
            return ret;
        }

        public PartnerActionMap Update(PartnerActionMap map)
        {
            var ret = _context.PartnerActionMap.Update(map).Entity;
            _context.SaveChanges();
            return ret;
        }

        public void Delete(int? id)
        {
            var map = _context.PartnerActionMap.Find(id);

            if (map == null)
                return;

            _context.PartnerActionMap.Remove(map);
            _context.SaveChanges();
        }
    }
}
