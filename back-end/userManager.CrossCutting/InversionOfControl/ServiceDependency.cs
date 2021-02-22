using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using userManager.Domain.Interfaces;
using userManager.Service.Services;

namespace userManager.Infra.CrossCutting.InversionOfControl
{
    public static class ServiceDependency
    {
        public static void AddServiceDependency(this IServiceCollection services)
        {
            services.AddScoped<IServiceAuthorization, ServiceAuthorization>();
        }
    }
}
