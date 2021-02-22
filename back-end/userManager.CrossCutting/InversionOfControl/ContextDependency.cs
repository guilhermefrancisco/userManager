using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using userManager.Infra.Data.Context;

namespace userManager.Infra.CrossCutting.InversionOfControl
{
    public static class ContextDependency
    {
        public static void AddContextDependency(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<UserManagerContext>(options => 
            {
                options.UseSqlServer(config.GetConnectionString("userManagerDB"), opt => 
                {
                    opt.CommandTimeout(180);
                    opt.EnableRetryOnFailure(5);
                });
            });
        }
    }
}
