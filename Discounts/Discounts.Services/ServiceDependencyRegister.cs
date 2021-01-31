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
        }
    }
}
