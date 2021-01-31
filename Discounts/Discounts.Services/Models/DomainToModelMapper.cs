using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Discounts.DataLayer.Models;

namespace Discounts.Services.Models
{
    public class DomainToModelMapper : Profile
    {
        public DomainToModelMapper()
        {
            CreateMap<PartnerType, PartnerTypeModel>();
            CreateMap<PartnerTypeModel, PartnerType>();

            CreateMap<Partner, PartnerModel>()
                .ForMember(d => d.PartnerTypeName, o => o.MapFrom(s => s.PartnerType.Name));
            CreateMap<PartnerModel, Partner>();

            CreateMap<DiscountAction, ActionModel>()
                .ForMember(d => d.IsCanceled, o => o.MapFrom(x => x.IsCanceled ?? false));
            CreateMap<ActionModel, DiscountAction>();

            CreateMap<PartnerActionMap, PartnerActionMapModel>()
                .ForMember(d => d.PartnerName, o => o.MapFrom(s => s.Partner.Name))
                .ForMember(d => d.ActionName, o => o.MapFrom(s => s.Action.Name));
            CreateMap<PartnerActionMapModel, PartnerActionMap>();

            CreateMap<UsedAction, UsedActionModel>()
                .ForMember(d => d.ActionName, o => o.MapFrom(s => s.Action.Name))
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.User.UserName))
                .ForMember(d => d.PartnerName, o => o.MapFrom(s => s.Partner.Name));
            CreateMap<UsedActionModel, UsedAction>();

            CreateMap<DiscountsUser, UserModel>()
                .ForMember(d => d.PartnerName, o => o.MapFrom(s => s.Partner.Name))
                .ForMember(d => d.Roles, o => o.MapFrom(s => s.UserRoleMaps.Select(x => x.Role.Name).ToList()));

            CreateMap<DiscountsRole, RoleModel>();
        }
    }
}
