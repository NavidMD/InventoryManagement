using InventoryManagement.Core.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Core
{
    public static class CoreDependencyInjections
    {
        public static IServiceCollection AddCoreDI(this IServiceCollection services, IConfiguration configurations)
        {
            services.Configure<ConnectionStringOptions>(configurations.GetSection(ConnectionStringOptions.SectionName));
            return services;
        }
    }
}
