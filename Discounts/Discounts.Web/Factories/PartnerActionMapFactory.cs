using AutoMapper;
using Discounts.DataLayer.Models;
using Discounts.Services.Helpers;
using Discounts.Services.Interfaces;
using Discounts.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discounts.Web.Factories
{
    public class PartnerActionMapFactory
    {
        private readonly IPartnerActionMapService _service;
        private readonly IPartnerService _partnerService;
        private readonly IActionService _actionService;
        private readonly IMapper _mapper;

        public PartnerActionMapFactory(IPartnerActionMapService service, IPartnerService partnerService, IActionService actionService, IMapper mapper)
        {
            _service = service;
            _partnerService = partnerService;
            _actionService = actionService;
            _mapper = mapper;
        }

        public IEnumerable<PartnerActionMapModel> GetAllMaps()
        {
            return _service.GetPartnerActionMaps().Select(x => _mapper.Map<PartnerActionMap, PartnerActionMapModel>(x));
        }

        public PartnerActionMapModel GetMap(int? id)
        {
            return _mapper.Map<PartnerActionMap, PartnerActionMapModel>(_service.GetPartnerActionMap(id));
        }

        public PartnerActionMapModel CreateMap(PartnerActionMapModel map)
        {
            if (!_partnerService.Exists(map.PartnerId))
                throw new InvalidOperationException(ServicesConstants.CreatePartnerActionMap_NotAllowedReasonMessage_PartnerDoesNotExist);
            if (!_actionService.Exists(map.ActionId))
                throw new InvalidOperationException(ServicesConstants.CreatePartnerActionMap_NotAllowedReasonMessage_ActionDoesNotExist);

            if (_service.PartnerActionMapExists(map.PartnerId, map.ActionId))
                throw new InvalidOperationException(ServicesConstants.CreatePartnerActionMap_NotAllowedReasonMessage_MapAlreadyExists);

            var nMap = _mapper.Map<PartnerActionMapModel, PartnerActionMap>(map);
            nMap.CreatedDate = DateTime.UtcNow;

            var ret = _service.Create(nMap);

            return _mapper.Map<PartnerActionMap, PartnerActionMapModel>(ret);
        }

        public PartnerActionMapModel UpdateMap(PartnerActionMapModel map)
        {
            if (!_partnerService.Exists(map.PartnerId))
                throw new InvalidOperationException(ServicesConstants.UpdatePartnerActionMap_NotAllowedReasonMessage_PartnerDoesNotExist);
            if (!_actionService.Exists(map.ActionId))
                throw new InvalidOperationException(ServicesConstants.UpdatePartnerActionMap_NotAllowedReasonMessage_ActionDoesNotExist);

            if (_service.PartnerActionMapExists(map.PartnerId, map.ActionId))
                throw new InvalidOperationException(ServicesConstants.UpdatePartnerActionMap_NotAllowedReasonMessage_MapAlreadyExists);

            var dMap = _service.GetPartnerActionMap(map.Id);

            dMap.PartnerId = map.PartnerId;
            dMap.ActionId = map.ActionId;

            var ret = _service.Update(dMap);

            return _mapper.Map<PartnerActionMap, PartnerActionMapModel>(ret);
        }

        public void DeleteMap(int? id)
        {
            _service.Delete(id);
        }
    }
}
