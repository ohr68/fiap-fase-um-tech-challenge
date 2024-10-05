using FIAP.FaseUm.TechChallenge.Infra.Data.Context;
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
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TechChallengeFaseUmDbContext>));
                
                if (descriptor is not null)
                    services.Remove(descriptor);

                services.AddDbContext<TechChallengeFaseUmDbContext>(options => options.UseInMemoryDatabase("InMemoryDbForTesting"));
            });
        }
    }
}
