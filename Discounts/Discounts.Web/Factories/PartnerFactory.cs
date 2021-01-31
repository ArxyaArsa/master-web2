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

        public IEnumerable<PartnerModel> GetAll()
        {
            return _partnerService.GetPartners().Select(x => _mapper.Map<Partner, PartnerModel>(x));
        }

        public PartnerModel GetPartner(int? id)
        {
            return _mapper.Map<Partner, PartnerModel>(_partnerService.GetPartners().FirstOrDefault(x => x.Id == id));
        }

        public PartnerModel CreatePartner(PartnerModel partner)
        {
            var ret = _partnerService.Create(_mapper.Map<PartnerModel, Partner>(partner));

            return _mapper.Map<Partner, PartnerModel>(ret);
        }

        public PartnerModel UpdatePartner(PartnerModel partner)
        {
            var dPartner = _partnerService.GetPartners().FirstOrDefault(x => x.Id == partner.Id);

            dPartner.Name = partner.Name;
            dPartner.StartDate = partner.StartDate;
            dPartner.EndDate = partner.EndDate;
            dPartner.PartnerTypeId = partner.PartnerTypeId;

            var ret = _partnerService.Update(dPartner);

            return _mapper.Map<Partner, PartnerModel>(ret);
        }

        public void DeletePartner(int? id)
        {
            _partnerService.Delete(id);
        }
    }
}
