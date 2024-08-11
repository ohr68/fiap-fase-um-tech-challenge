using FIAP.FaseUm.TechChallenge.Domain.Interfaces.Repositories;
using FIAP.FaseUm.TechChallenge.Domain.Interfaces.Services;
using FIAP.FaseUm.TechChallenge.Domain.Tests.Fixtures;
using FIAP.FaseUm.TechChallenge.Domain.Tests.Helpers;
using FIAP.FaseUm.TechChallenge.Infra.Data.Context;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

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
        [TestOrder(Order = 1)]
        public async Task ContatoService_CadastrarContato()
        {
            // Arrange
            using (var scope = _contatoServiceFixture.ServiceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var contatoRepository = scopedServices.GetRequiredService<IContatoRepository>();
                var contatoService = scopedServices.GetRequiredService<IContatoService>();
                var contato = _contatoFixture.GerarContatoValido();

                // Act
                contatoService.CadastrarContato(contato);

                // Assert
                var contatoAdicionado = await contatoRepository.GetById(contato.Id);
                _contatoServiceFixture.SetContatoAdicionado(contatoAdicionado);
                contatoAdicionado.Should().NotBeNull();
            }
        }

        [Fact(DisplayName = "Alterar contato com sucesso.")]
        [Trait("", "ContatoService")]
        [TestOrder(Order = 2)]
        public async Task ContatoService_AlterarContato()
        {
            // Arrange
            using (var scope = _contatoServiceFixture.ServiceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var contatoRepository = scopedServices.GetRequiredService<IContatoRepository>();
                var contatoService = scopedServices.GetRequiredService<IContatoService>();
                var contato = _contatoFixture.GerarContatoValido();
                var contatoExistente = _contatoServiceFixture.ContatoAdicionado;

                // Act
                await contatoService.AlterarContato(contatoExistente!.Id, contato);

                // Assert
                var contatoAlterado = await contatoRepository.GetById(contatoExistente.Id);
                contatoAlterado.Should().NotBeNull();
                contatoAlterado!.Nome.Should().Be(contato.Nome);
                contatoAlterado.Telefone.Numero.Should().Be(contato.Telefone.Numero);
                contatoAlterado.Telefone.Ddd.Should().Be(contato.Telefone.Ddd);
                contatoAlterado.Email.Endereco.Should().Be(contato.Email.Endereco);
                _contatoServiceFixture.SetContatoAdicionado(contatoAlterado);
            }
        }

        [Fact(DisplayName = "Listar contatos com sucesso.")]
        [Trait("", "ContatoService")]
        [TestOrder(Order = 3)]
        public async Task ContatoService_ListarContatos()
        {
            // Arrange
            using (var scope = _contatoServiceFixture.ServiceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var contatoRepository = scopedServices.GetRequiredService<IContatoRepository>();
                var contatoService = scopedServices.GetRequiredService<IContatoService>();
                var contatoExistente = _contatoServiceFixture.ContatoAdicionado;

                // Act
                var contatos = await contatoService.ListarContatos(contatoExistente!.Telefone.Ddd);

                // Assert
                contatos.Should().NotBeEmpty();
                contatos.Should().HaveCountGreaterThanOrEqualTo(1);
            }
        }

        [Fact(DisplayName = "Remover contato com sucesso.")]
        [Trait("", "ContatoService")]
        [TestOrder(Order = 4)]
        public async Task ContatoService_RemoverContato()
        {
            // Arrange
            using (var scope = _contatoServiceFixture.ServiceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var contatoRepository = scopedServices.GetRequiredService<IContatoRepository>();
                var contatoService = scopedServices.GetRequiredService<IContatoService>();
                var contatoExistente = _contatoServiceFixture.ContatoAdicionado;

                // Act
                await contatoService.RemoverContato(contatoExistente!.Id);

                // Assert
                var contatoRemovido = await contatoRepository.GetById(contatoExistente.Id);
                contatoRemovido.Should().BeNull();
            }
        }
    }
}
