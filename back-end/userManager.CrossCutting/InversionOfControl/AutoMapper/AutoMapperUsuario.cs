using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using userManager.Domain.DTOs.Usuario;

namespace userManager.Infra.CrossCutting.InversionOfControl.AutoMapper
{
    public static class AutoMapperUsuario
    {
        public static void AddAutoMapperUsuario(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<RegisterUserDTO,LoginUserDTO>();
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
