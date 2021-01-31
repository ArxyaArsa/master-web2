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
    public class PartnerTypeFactory
    {
        private readonly IPartnerTypeService _partnerTypeService;
        private readonly IMapper _mapper;

        public PartnerTypeFactory(IPartnerTypeService partnerTypeService, IMapper mapper)
        {
            _partnerTypeService = partnerTypeService;
            _mapper = mapper;
        }

        public IEnumerable<PartnerTypeModel> GetAllPartnerTypes()
        {
            return _partnerTypeService.GetPartnerTypes().Select(x => _mapper.Map<PartnerType, PartnerTypeModel>(x));
        }

        public PartnerTypeModel GetPartnerType(int? id)
        {
            return GetAllPartnerTypes().FirstOrDefault(x => x.Id == id);
        }
    }
}
