using DataAccess.Interface;
using DataAccess.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public static class IocConfig
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IBreweryRepository, BreweryRepository>();
            return services;
        }
    }
}
