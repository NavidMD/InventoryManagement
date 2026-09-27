using InventoryManagement.Application;
using InventoryManagement.Core;
using InventoryManagement.Infrastructure;

namespace InventoryManagement.API
{
    public static class ProjectDependencyInjections
    {
        public static IServiceCollection AddProjectDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationDI();
            services.AddInfrastructureDI();
            services.AddCoreDI(configuration);
            return services;
        }
    }
}
