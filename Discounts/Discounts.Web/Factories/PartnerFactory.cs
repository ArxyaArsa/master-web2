using AutoMapper;
using Discounts.DataLayer.Models;
using Discounts.Services.Interfaces;
using Discounts.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discounts.Web.Factories
{
    public class PartnerFactory
    {
        private readonly IPartnerService _partnerService;
        private readonly IMapper _mapper;

        public PartnerFactory(IPartnerService partnerService, IMapper mapper)
        {
            _partnerService = partnerService;
            _mapper = mapper;
        }

        public IEnumerable<PartnerModel> GetAllPartners()
        {
            return _partnerService.GetPartners().Select(x => _mapper.Map<Partner, PartnerModel>(x));
        }
    }
}
