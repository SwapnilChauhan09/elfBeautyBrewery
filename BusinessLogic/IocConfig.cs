using BusinessLogics.Interface;
using BusinessLogics.Manager;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogics
{
    public static class IocConfig
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IBreweryManager, BreweryManager>();
            DataAccess.IocConfig.ConfigureServices(services);

            return services;
        }
    }
}
