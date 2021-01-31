using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discounts.Web.Factories
{
    public static class FactoryRegister
    {
        public static void AddFactories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<UserFactory>();
        }
    }
}
