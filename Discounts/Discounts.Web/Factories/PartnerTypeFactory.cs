using AutoMapper;
using Discounts.DataLayer.Models;
using Discounts.Services.Interfaces;
using Discounts.Services.Models;
using Discounts.Web.Areas.User.Models;
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

        public PartnerTypeModel CraetePartnerType(PartnerTypeModel model)
        {
            var partner = _mapper.Map<PartnerTypeModel, PartnerType>(model);

            return _mapper.Map<PartnerType, PartnerTypeModel>(_partnerTypeService.Create(partner));
        }

        public PartnerTypeModel UpdatePartnerType(PartnerTypeModel partnerType)
        {
            var dPartnerType = _partnerTypeService.GetPartnerTypes().FirstOrDefault(x => x.Id == partnerType.Id);

            dPartnerType.Name = partnerType.Name;
            var ret = _partnerTypeService.Update(dPartnerType);

            return _mapper.Map<PartnerType, PartnerTypeModel>(ret);
        }

        public void DeletePartnerType(int? id)
        {
            _partnerTypeService.Delete(id);
        }

        #region User Area

        public IEnumerable<ViewByCategoryModel> GetCategoriesForViewByCategory()
        {
            return _partnerTypeService.GetPartnerTypes().Select(x => new ViewByCategoryModel()
            {
                Id = x.Id,
                Name = x.Name,
                PartnerCount = x.Partners.Count(),
                ActionCount = x.Partners.Select(y => y.PartnerActionMaps.Count()).Sum()
            });
        }

        #endregion
    }
}
