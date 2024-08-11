using FIAP.FaseUm.TechChallenge.Domain.Entities;
using FIAP.FaseUm.TechChallenge.Domain.Interfaces.Repositories;
using FIAP.FaseUm.TechChallenge.Domain.Interfaces.Services;
using FIAP.FaseUm.TechChallenge.Domain.Services;
using FIAP.FaseUm.TechChallenge.Infra.Data.Context;
using FIAP.FaseUm.TechChallenge.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.FaseUm.TechChallenge.Domain.Tests.Fixtures
{
    public class ContatoServiceFixture
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public Contato? ContatoAdicionado { get; set; }

        public ContatoServiceFixture()
        {
            ServiceProvider = Setup();
        }

        private IServiceProvider Setup()
        {
            var services = new ServiceCollection();

            // Using In-Memory database for testing
            services.AddDbContext<TechChallengeFaseUmDbContext>(options =>
                options.UseInMemoryDatabase("TechChallengeDb"));

            services.AddScoped<IContatoRepository, ContatoRepository>();
            services.AddScoped<IContatoService, ContatoService>();

            return services.BuildServiceProvider();
        }

        public void SetContatoAdicionado(Contato contatoAdicionado) 
            => ContatoAdicionado = contatoAdicionado;
    }
}
