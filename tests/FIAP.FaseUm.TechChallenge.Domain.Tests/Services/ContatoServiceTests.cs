using FIAP.FaseUm.TechChallenge.Domain.Interfaces.Messaging;
using FIAP.FaseUm.TechChallenge.Domain.Interfaces.Repositories;
using FIAP.FaseUm.TechChallenge.Domain.Interfaces.Services;
using FIAP.FaseUm.TechChallenge.Domain.Services;
using FIAP.FaseUm.TechChallenge.Domain.Tests.Fixtures;
using FIAP.FaseUm.TechChallenge.Domain.Tests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace FIAP.FaseUm.TechChallenge.Domain.Tests.Services
{
    [TestCaseOrderer("FIAP.FaseUm.TechChallenge.Domain.Tests.Helpers.TestsOrderer", "FIAP.FaseUm.TechChallenge.Domain.Tests")]
    public class ContatoServiceTests : IClassFixture<ContatoServiceFixture>, IClassFixture<ContatoFixture>
    {
        private readonly ContatoServiceFixture _contatoServiceFixture;
        private readonly ContatoFixture _contatoFixture;

        public ContatoServiceTests(ContatoServiceFixture contatoServiceFixture, ContatoFixture contatoFixture)
        {
            _contatoServiceFixture = contatoServiceFixture;
            _contatoFixture = contatoFixture;
        }

        [Fact(DisplayName = "Cadastrar contato com sucesso.")]
        [Trait("", "ContatoService")]
        public async Task ContatoService_CadastrarContato()
        {
            // Arrange
            using (var scope = _contatoServiceFixture.ServiceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var contatoRepository = scopedServices.GetRequiredService<IContatoRepository>();
                var queueServiceMock = new Mock<IQueueService>();
                var contatoService = new ContatoService(contatoRepository, queueServiceMock.Object);
                var contato = _contatoFixture.GerarContatoValido();

                // Act
                await contatoService.CadastrarContato(contato);

                // Assert
                queueServiceMock.Verify(q => q.Publish(It.IsAny<object>(), It.IsAny<CancellationToken>()), Times.Once);
            }
        }

        [Fact(DisplayName = "Alterar contato com sucesso.")]
        [Trait("", "ContatoService")]
        public async Task ContatoService_AlterarContato()
        {
            // Arrange
            using (var scope = _contatoServiceFixture.ServiceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var contatoRepository = scopedServices.GetRequiredService<IContatoRepository>();
                var queueServiceMock = new Mock<IQueueService>();
                var contatoService = new ContatoService(contatoRepository, queueServiceMock.Object);
                var contato = _contatoFixture.GerarContatoValido();

                // Act
                await contatoService.AlterarContato(1, contato);

                // Assert
                // Assert
                queueServiceMock.Verify(q => q.Publish(It.IsAny<object>(), It.IsAny<CancellationToken>()), Times.Once);
            }
        }

        [Fact(DisplayName = "Remover contato com sucesso.")]
        [Trait("", "ContatoService")]
        public async Task ContatoService_RemoverContato()
        {
            // Arrange
            using (var scope = _contatoServiceFixture.ServiceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var contatoRepository = scopedServices.GetRequiredService<IContatoRepository>();
                var queueServiceMock = new Mock<IQueueService>();
                var contatoService = new ContatoService(contatoRepository, queueServiceMock.Object);

                // Act
                await contatoService.RemoverContato(1);

                // Assert
                queueServiceMock.Verify(q => q.Publish(It.IsAny<object>(), It.IsAny<CancellationToken>()), Times.Once);
            }
        }
    }
}
