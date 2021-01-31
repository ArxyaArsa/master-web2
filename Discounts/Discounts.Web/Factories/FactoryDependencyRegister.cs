using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discounts.Web.Factories
{
    public static class FactoryDependencyRegister
    {
        public static void AddFactoryDependencies(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<UserFactory>();
            serviceCollection.AddScoped<PartnerFactory>();
        }
    }
}
