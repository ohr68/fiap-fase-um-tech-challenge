using FIAP.FaseUm.TechChallenge.Domain.Interfaces.Repositories;
using FIAP.FaseUm.TechChallenge.Infra.Data.Context;
using FIAP.FaseUm.TechChallenge.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.FaseUm.TechChallenge.Infra.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfraLayer(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
        {
            services
                 .AddDatabase(configuration, isDevelopment)
                 .AddRepositories();

            return services;
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
        {
            services.AddDbContext<TechChallengeFaseUmDbContext>(
                options =>
                {
                    var connectionString = configuration.GetConnectionString("TechChallengeFaseUm")!;
                    if (!isDevelopment)
                    {
                        var password = Environment.GetEnvironmentVariable("SA_PASSWORD");
                        connectionString = string.Format(connectionString, password);
                    }
                    options.UseSqlServer(connectionString);

                });

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services
                .AddScoped<IContatoRepository, ContatoRepository>();

            return services;
        }
    }
}
