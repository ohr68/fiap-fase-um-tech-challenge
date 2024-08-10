using FIAP.FaseUm.TechChallenge.Application.AppServices;
using FIAP.FaseUm.TechChallenge.Application.Interfaces.AppServices;
using FIAP.FaseUm.TechChallenge.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.FaseUm.TechChallenge.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services
                .AddValidation()
                .AddScoped<IContatoAppService, ContatoAppService>();

            return services;
        }

        private static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services
                .AddValidatorsFromAssemblyContaining<CadastroContatoValidator>();

            return services;
        }
    }
}