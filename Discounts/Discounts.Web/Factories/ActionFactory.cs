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
    public class ActionFactory
    {
        private readonly IActionService _actionService;
        private readonly IMapper _mapper;

        public ActionFactory(IActionService actionService, IMapper mapper)
        {
            _actionService = actionService;
            _mapper = mapper;
        }

        public IEnumerable<ActionModel> GetAllActions()
        {
            return _actionService.GetActions().Select(x => _mapper.Map<DiscountAction, ActionModel>(x));
        }

        public ActionModel GetAction(int? id)
        {
            return _mapper.Map<DiscountAction, ActionModel>(_actionService.GetAction(id));
        }

        public ActionModel CreateAction(ActionModel model)
        {
            var partner = _mapper.Map<ActionModel, DiscountAction>(model);

            partner.CreatedDate = DateTime.UtcNow;

            return _mapper.Map<DiscountAction, ActionModel>(_actionService.Create(partner));
        }

        public ActionModel UpdateAction(ActionModel partnerType)
        {
            var dPartnerType = _actionService.GetAction(partnerType.Id);

            dPartnerType.Name = partnerType.Name;
            dPartnerType.Description = partnerType.Description;
            dPartnerType.EndDate = partnerType.StartDate;
            dPartnerType.IsCanceled = partnerType.IsCanceled;
            dPartnerType.CancelDate = partnerType.IsCanceled == false ? (DateTime?)null : (dPartnerType.CancelDate ?? DateTime.UtcNow);
            dPartnerType.CancelReason = partnerType.CancelReason;

            var ret = _actionService.Update(dPartnerType);
            return _mapper.Map<DiscountAction, ActionModel>(ret);
        }

        public void DeleteAction(int? id)
        {
            _actionService.Delete(id);
        }
    }
}
