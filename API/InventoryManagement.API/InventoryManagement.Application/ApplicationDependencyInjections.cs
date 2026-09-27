using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application
{
    public static class ApplicationDependencyInjections
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            //معرفی مدیاتور در لایه اپلیکیشن انجام میشه
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationDependencyInjections).Assembly));
            return services;
        }
    }
}
