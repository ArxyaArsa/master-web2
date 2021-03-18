using Discounts.Services.Interfaces;
using Discounts.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace Discounts.Services
{
    public static class ServiceDependencyRegister
    {
        public static void AddServiceDependencies(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IPartnerService, PartnerService>();
            serviceCollection.AddScoped<IPartnerTypeService, PartnerTypeService>();
            serviceCollection.AddScoped<IActionService, ActionService>();
            serviceCollection.AddScoped<IPartnerActionMapService, PartnerActionMapService>();
            serviceCollection.AddScoped<IUsedActionService, UsedActionService>();
            serviceCollection.AddScoped<IReportService, ReportService>();
        }
    }
}
