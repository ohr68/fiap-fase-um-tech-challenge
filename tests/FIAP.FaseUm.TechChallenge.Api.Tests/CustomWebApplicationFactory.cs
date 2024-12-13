using FIAP.FaseUm.TechChallenge.Infra.Data.Context;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.FaseUm.TechChallenge.Api.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TechChallengeFaseUmDbContext>));
                
                if (dbContextDescriptor is not null)
                    services.Remove(dbContextDescriptor);
                
                var massTransitDescriptors = services
                    .Where(d => d.ServiceType.Namespace != null && d.ServiceType.Namespace.Contains("MassTransit"))
                    .ToList();

                foreach (var descriptor in massTransitDescriptors)
                    services.Remove(descriptor);
                
                services.AddMassTransit(x =>
                {
                    x.UsingInMemory((context, cfg) =>
                    {
                        cfg.ConfigureEndpoints(context);
                    });
                });

                services.AddDbContext<TechChallengeFaseUmDbContext>(options => options.UseInMemoryDatabase("InMemoryDbForTesting"));
            });
        }
    }
}
