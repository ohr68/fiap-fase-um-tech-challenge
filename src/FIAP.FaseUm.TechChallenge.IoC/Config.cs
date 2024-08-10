using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FIAP.FaseUm.TechChallenge.Application.Extensions;
using FIAP.FaseUm.TechChallenge.Infra.Data.Extensions;
using FIAP.FaseUm.TechChallenge.Domain.Interfaces.Services;
using FIAP.FaseUm.TechChallenge.Domain.Services;

namespace FIAP.FaseUm.TechChallenge.IoC
{
    public static class Config
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services
                .AddApplicationLayer()
                .AddDomainLayer()
                .AddInfraLayer(configuration);

            return services;
        }

        private static IServiceCollection AddDomainLayer(this IServiceCollection services)
        {
            services.AddScoped<IContatoService, ContatoService>();

            return services;
        }
    }
}
