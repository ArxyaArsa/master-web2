using AutoMapper;
using Discounts.DataLayer.Models;
using Discounts.Services.Helpers;
using Discounts.Services.Interfaces;
using Discounts.Services.Models;
using Discounts.Web.Areas.Partner.Models;
using Discounts.Web.Areas.User.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discounts.Web.Factories
{
    public class PartnerActionMapFactory
    {
        #region dependencies and constructor
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
        #endregion

        #region Admin Area
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
#endregion

        #region User Area
        public IEnumerable<ViewByActionModel> GetActionsForViewByAction(int partnerId, int userId)
        {
            return _service.GetPartnerActionMaps().Where(x => x.PartnerId == partnerId).Select(x => new ViewByActionModel()
            {
                Id = x.Action.Id,
                Name = x.Action.Name,
                StartDate = x.Action.StartDate,
                EndDate = x.Action.EndDate,
                CancelDate = x.Action.CancelDate,
                CancelReason = x.Action.CancelReason,
                CashValue = x.Action.CashValue,
                CreatedDate = x.Action.CreatedDate,
                Description = x.Action.Description,
                IsCanceled = x.Action.IsCanceled ?? false,
                IsFinished = x.Action.EndDate < DateTime.Now,
                IsUsed = x.Action.UsedActions.Where(y => y.UserId == userId && y.ActionId == x.ActionId).Count() > 0,
                PercentValue = x.Action.PercentValue
            });
        }

        public ViewByActionModel GetActionForActionDetailsView(int partnerId, int userId, int actionId)
        {
            return GetActionsForViewByAction(partnerId, userId).Where(x => x.Id == actionId).FirstOrDefault();
        }
        #endregion

        #region Partner Area
        public IEnumerable<ActionViewModel> GetActionsForPartnerActionsView(int partnerId, int userId)
        {
            return _service.GetPartnerActionMaps().Where(x => x.PartnerId == partnerId).Select(x => new ActionViewModel()
            {
                Id = x.Action.Id,
                Name = x.Action.Name,
                StartDate = x.Action.StartDate,
                EndDate = x.Action.EndDate,
                CancelDate = x.Action.CancelDate,
                CancelReason = x.Action.CancelReason,
                CashValue = x.Action.CashValue,
                CreatedDate = x.Action.CreatedDate,
                Description = x.Action.Description,
                IsCanceled = x.Action.IsCanceled ?? false,
                IsFinished = x.Action.EndDate < DateTime.Now,
                IsUsed = x.Action.UsedActions.Where(y => y.UserId == userId && y.ActionId == x.ActionId).Count() > 0,
                PercentValue = x.Action.PercentValue
            });
        }

        public ActionViewModel GetActionForPartnerActionDetailsView(int partnerId, int userId, int actionId)
        {
            return GetActionsForPartnerActionsView(partnerId, userId).Where(x => x.Id == actionId).FirstOrDefault();
        }
        #endregion
    }
}
