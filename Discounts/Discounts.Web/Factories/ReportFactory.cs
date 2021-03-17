using Discounts.Services.Interfaces;
using Discounts.Web.Models.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discounts.Web.Factories
{
    public class ReportFactory
    {
        #region construct and dependencies
        private readonly IUsedActionService _usedActionservice;
        private readonly IPartnerService _partnerService;
        private readonly IActionService _actionService;
        private readonly IUserService _userService;

        public ReportFactory(IUsedActionService usedActionservice, IPartnerService partnerService, IActionService actionService, IUserService userService)
        {
            _usedActionservice = usedActionservice;
            _partnerService = partnerService;
            _actionService = actionService;
            _userService = userService;
        }
        #endregion

        public IEnumerable<ReportRecord> GenerateReportData(ReportFilterModel filter)
        {
            var entries = _usedActionservice.GetUsedActions().AsQueryable().ApplyFilter(filter);

            return entries.Select(x => new
            {
                action = filter.GroupByAction ? x.Action.Name : null,
                user = filter.GroupByUser ? x.User.UserName : null,
                partner = filter.GroupByPartner ? x.Partner.Name : null,
                partnerType = filter.GroupByPartnerType ? x.Partner.PartnerType.Name : null,
                originalValue = x.OriginalValue,
                actionValue = x.ActionValue
            }).GroupBy(g => new { g.action, g.user, g.partner, g.partnerType })
            .Select(g => new ReportRecord()
            {
                ActionName = g.Key.action,
                UserName = g.Key.user,
                PartnerName = g.Key.partner,
                PartnerTypeName = g.Key.partnerType,
                OriginalValue = g.Sum(x => x.originalValue),
                ActionValue = g.Sum(x => x.actionValue)
            }).AsEnumerable();
        }
    }

    public static class ReportFilterExtensions
    {
        public static IQueryable<DataLayer.Models.UsedAction> ApplyFilter(this IQueryable<DataLayer.Models.UsedAction> source, ReportFilterModel filter)
        {
            if (filter.PartnerIds != null && filter.PartnerIds.Any())
                source = source.Where(x => filter.PartnerIds.Contains(x.PartnerId));

            if (filter.ActionIds != null && filter.ActionIds.Any())
                source = source.Where(x => filter.ActionIds.Contains(x.ActionId));

            if (filter.UserIds != null && filter.UserIds.Any())
                source = source.Where(x => filter.UserIds.Contains(x.UserId));

            if (filter.PartnerTypeIds != null && filter.PartnerTypeIds.Any())
                source = source.Where(x => x.Partner != null && filter.PartnerTypeIds.Contains(x.Partner.PartnerTypeId));

            if (filter.DateFrom != null)
                source = source.Where(x => x.DateCreated != null && filter.DateFrom >= x.DateCreated);

            if (filter.DateTo != null)
                source = source.Where(x => x.DateCreated != null && filter.DateTo <= x.DateCreated);

            return source;
        }
    }
}
