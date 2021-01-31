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
    public class PartnerService : IPartnerService
    {
        #region dependencies & constructor
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PartnerService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        public IEnumerable<Partner> GetPartners()
        {
            return _context.Partner.AsEnumerable();
        }

        public Partner Create(Partner partner)
        {
            var ret = _context.Partner.Add(partner).Entity;
            _context.SaveChanges();
            return ret;
        }

        public Partner Update(Partner partner)
        {
            var ret = _context.Partner.Update(partner).Entity;
            _context.SaveChanges();
            return ret;
        }

        public void Delete(int? id)
        {
            var partner = _context.Partner.Find(id);

            if (partner == null)
                return;

            if (partner.Users.Count > 0)
                throw new InvalidOperationException(ServicesConstants.DeletePartner_NotAllowedReasonMessage_PartnerHasUsers);
            if (partner.PartnerActionMaps.Count > 0)
                throw new InvalidOperationException(ServicesConstants.DeletePartner_NotAllowedReasonMessage_PartnerHasActions);
            if (partner.UsedActions.Count > 0)
                throw new InvalidOperationException(ServicesConstants.DeletePartner_NotAllowedReasonMessage_PartnerHasUsedActions);

            _context.Partner.Remove(partner);
            _context.SaveChanges();
        }

        public bool Exists(int? id)
        {
            return _context.Partner.Any(x => x.Id == id);
        }
    }
}
