using Microsoft.Extensions.DependencyInjection;

namespace Discounts.Web.Factories
{
    public static class FactoryDependencyRegister
    {
        public static void AddFactoryDependencies(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<UserFactory>();
            serviceCollection.AddScoped<PartnerFactory>();
            serviceCollection.AddScoped<PartnerTypeFactory>();
            serviceCollection.AddScoped<ActionFactory>();
            serviceCollection.AddScoped<PartnerActionMapFactory>();
            serviceCollection.AddScoped<UsedActionFactory>();
        }
    }
}
