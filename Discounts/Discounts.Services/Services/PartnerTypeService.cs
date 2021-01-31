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
    public class PartnerTypeService : IPartnerTypeService
    {
        #region dependencies & constructor
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PartnerTypeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        public IEnumerable<PartnerType> GetPartnerTypes()
        {
            return _context.PartnerType.AsEnumerable();
        }

        public PartnerType GetPartnerType(int? id)
        {
            return _context.PartnerType.Find(id);
        }

        public PartnerType Create(PartnerType model)
        {
            var ret = _context.PartnerType.Add(model).Entity;
            _context.SaveChanges();
            return ret;
        }

        public PartnerType Update(PartnerType model)
        {
            var ret = _context.PartnerType.Update(model).Entity;
            _context.SaveChanges();
            return ret;
        }

        public void Delete(int? id)
        {
            var partnerType = _context.PartnerType.Find(id);

            if (partnerType == null)
                return;

            if (partnerType.Partners.Count > 0)
                throw new InvalidOperationException(ServicesConstants.DeletePartnerType_NotAllowedReasonMessage_PartnerTypeHasPartners);

            _context.PartnerType.Remove(partnerType);
            _context.SaveChanges();
        }
    }
}
