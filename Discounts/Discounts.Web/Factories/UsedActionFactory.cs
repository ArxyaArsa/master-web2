using AutoMapper;
using Discounts.DataLayer.Models;
using Discounts.Services.Helpers;
using Discounts.Services.Interfaces;
using Discounts.Services.Models;
using Discounts.Web.Areas.Partner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discounts.Web.Factories
{
    public class UsedActionFactory
    {
        #region construct and dependencies
        private readonly IUsedActionService _service;
        private readonly IPartnerService _partnerService;
        private readonly IActionService _actionService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsedActionFactory(IUsedActionService service, IPartnerService partnerService, IActionService actionService, IUserService userService, IMapper mapper)
        {
            _service = service;
            _partnerService = partnerService;
            _actionService = actionService;
            _mapper = mapper;
            _userService = userService;
        }
        #endregion

        #region Admin
        public IEnumerable<UsedActionModel> GetAll()
        {
            return _service.GetUsedActions().Select(x => _mapper.Map<UsedAction, UsedActionModel>(x));
        }

        public UsedActionModel Get(int? id)
        {
            return _mapper.Map<UsedAction, UsedActionModel>(_service.GetUsedAction(id));
        }

        public UsedActionModel Create(UsedActionModel model)
        {
            if (!_partnerService.Exists(model.PartnerId))
                throw new InvalidOperationException(ServicesConstants.CreateUsedAction_NotAllowedReasonMessage_PartnerDoesNotExist);
            if (!_actionService.Exists(model.ActionId))
                throw new InvalidOperationException(ServicesConstants.CreateUsedAction_NotAllowedReasonMessage_ActionDoesNotExist);
            if (!_userService.Exists(model.UserId))
                throw new InvalidOperationException(ServicesConstants.CreateUsedAction_NotAllowedReasonMessage_UserDoesNotExist);

            // remove this part if we want to allow multiple entries for same tripple
            // replace this part if we want to allow but limit
            if (_service.UsedActionExists(model.UserId, model.PartnerId, model.ActionId))
                throw new InvalidOperationException(ServicesConstants.CreateUsedAction_NotAllowedReasonMessage_MapAlreadyExists);

            var dModel = _mapper.Map<UsedActionModel, UsedAction>(model);
            //dModel.CreatedDate = DateTime.UtcNow;

            var ret = _service.Create(dModel);

            return _mapper.Map<UsedAction, UsedActionModel>(ret);
        }

        public UsedActionModel Update(UsedActionModel model)
        {
            if (!_partnerService.Exists(model.PartnerId))
                throw new InvalidOperationException(ServicesConstants.UpdateUsedAction_NotAllowedReasonMessage_PartnerDoesNotExist);
            if (!_actionService.Exists(model.ActionId))
                throw new InvalidOperationException(ServicesConstants.UpdateUsedAction_NotAllowedReasonMessage_ActionDoesNotExist);
            if (!_userService.Exists(model.UserId))
                throw new InvalidOperationException(ServicesConstants.UpdateUsedAction_NotAllowedReasonMessage_UserDoesNotExist);

            // remove this part if we want to allow multiple entries for same tripple
            // replace this part if we want to allow but limit
            var usedActions = _service.GetUsedActions(model.UserId, model.PartnerId, model.ActionId).Where(x => x.Id != model.Id).ToList();
            if (usedActions.Count > 0)
                throw new InvalidOperationException(ServicesConstants.UpdateUsedAction_NotAllowedReasonMessage_MapAlreadyExists);

            var dMap = _service.GetUsedAction(model.Id);

            dMap.PartnerId = model.PartnerId;
            dMap.ActionId = model.ActionId;
            dMap.UserId = model.UserId;

            var ret = _service.Update(dMap);

            return _mapper.Map<UsedAction, UsedActionModel>(ret);
        }

        public void DeleteMap(int? id)
        {
            _service.Delete(id);
        }
        #endregion

        #region Partner
        public IEnumerable<UsedActionViewModel> GetUsedActionsForPartnerUsedActionsView(int partnerId, int userId, int actionId)
        {
            return _service.GetUsedActions().Where(x => x.PartnerId == partnerId && x.ActionId == actionId).Select(x => new UsedActionViewModel()
            {
                ActionId = x.ActionId,
                ActionName = x.Action.Name,
                ActionValue = x.ActionValue,
                PartnerId = x.PartnerId,
                PartnerName = x.Partner.Name,
                UserId = x.UserId,
                UserName = x.User.UserName,
                Id = x.Id
            });
        }
        #endregion
    }
}
