using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using userManager.Infra.Data.Context;

namespace userManager.Infra.CrossCutting.InversionOfControl
{
    public static class IdentityDependency
    {
        public static void AddIdentityConfig(this IServiceCollection services)
        {
            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<UserManagerContext>()
                .AddDefaultTokenProviders();
        }
    }
}
