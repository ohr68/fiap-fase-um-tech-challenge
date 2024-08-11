using FIAP.FaseUm.TechChallenge.Application.AppServices;
using FIAP.FaseUm.TechChallenge.Application.Dto;
using FIAP.FaseUm.TechChallenge.Application.Tests.Fixtures;
using FIAP.FaseUm.TechChallenge.Application.Validation;
using FIAP.FaseUm.TechChallenge.Custom.Exceptions.Config;
using FIAP.FaseUm.TechChallenge.Domain.Entities;
using FIAP.FaseUm.TechChallenge.Domain.Interfaces.Services;
using FluentAssertions;
using Moq;

namespace FIAP.FaseUm.TechChallenge.Application.Tests.AppServices
{
    public class ContatoAppServiceTests : IClassFixture<ContatoAppServiceFixture>
    {
        private readonly ContatoAppServiceFixture _fixture;

        public ContatoAppServiceTests(ContatoAppServiceFixture contatoAppServiceTestsFixture)
        {
            _fixture = contatoAppServiceTestsFixture;
        }

        [Fact(DisplayName = "Cadastrar contato com sucesso.")]
        [Trait("ContatoAppService", "CadastroContato")]
        public async Task ContatoAppService_DeveCadastrarContato_QuandoContatoValido()
        {
            //Arrange
            var mockContatoService = new Mock<IContatoService>();
            mockContatoService.Setup(x => x.CadastrarContato(It.IsAny<Contato>()));
            var validatorCadastro = new CadastroContatoValidator();
            var contatoAppService = new ContatoAppService(mockContatoService.Object, validatorCadastro, null);
            var nome = _fixture.GerarNomeValido();
            var telefone = _fixture.GerarTelefoneValido();
            var email = _fixture.GerarEmailValido();
            CadastroContatoDto cadastroContatoDto = new()
            {
                nome = nome,
                telefone = telefone,
                email = email,
            };

            //Act
            var result = await contatoAppService.CadastrarContato(cadastroContatoDto);

            //Assert
            result.Should().NotBeNull();
            result.nome.Should().Be(nome);
            result.telefone.Should().Be(telefone);
            result.email.Should().Be(email);
        }

        [Fact(DisplayName = "Cadastrar contato com erro.")]
        [Trait("ContatoAppService", "CadastroContato")]
        public async Task ContatoAppService_NaoDeveCadastrarContato_QuandoContatoInvalido()
        {
            //Arrange
            var mockContatoService = new Mock<IContatoService>();
            mockContatoService.Setup(x => x.CadastrarContato(It.IsAny<Contato>()));
            var validator = new CadastroContatoValidator();
            var contatoAppService = new ContatoAppService(mockContatoService.Object, validator, null);
            var nome = _fixture.GerarNomeValido();
            var telefone = _fixture.GerarTelefoneValido();
            var email = _fixture.GerarEmailValido();
            CadastroContatoDto cadastroContatoDto = new()
            {
                nome = string.Empty,
                telefone = string.Empty,
                email = string.Empty,
            };

            //Act
            var act = async () => await contatoAppService.CadastrarContato(cadastroContatoDto);

            //Assert
            await act.Should().ThrowAsync<ValidationException>();
        }

        [Fact(DisplayName = "Alterar contato com sucesso.")]
        [Trait("ContatoAppService", "AlterarContato")]
        public async Task ContatoAppService_DeveAlterarContato_QuandoContatoValido()
        {
            //Arrange
            var mockContatoService = new Mock<IContatoService>();
            mockContatoService.Setup(x => x.AlterarContato(It.IsAny<int>(), It.IsAny<Contato>()));
            var validator = new AlteracaoContatoValidator();
            var contatoAppService = new ContatoAppService(mockContatoService.Object, null, validator);
            var nome = _fixture.GerarNomeValido();
            var telefone = _fixture.GerarTelefoneValido();
            var email = _fixture.GerarEmailValido();
            AlteracaoContatoDto alteracaoContatoDto = new()
            {
                nome = nome,
                telefone = telefone,
                email = email,
            };

            //Act
            var act = async () => await contatoAppService.AlterarContato(1, alteracaoContatoDto);

            //Assert
            await act.Should().NotThrowAsync<Exception>();
        }

        [Fact(DisplayName = "Alterar contato com erro.")]
        [Trait("ContatoAppService", "AlterarContato")]
        public async Task ContatoAppService_NaoDeveAlterarContato_QuandoContatoInvalido()
        {
            //Arrange
            var mockContatoService = new Mock<IContatoService>();
            mockContatoService.Setup(x => x.AlterarContato(It.IsAny<int>(), It.IsAny<Contato>()));
            var validator = new AlteracaoContatoValidator();
            var contatoAppService = new ContatoAppService(mockContatoService.Object, null, validator);
            var nome = _fixture.GerarNomeValido();
            var telefone = _fixture.GerarTelefoneValido();
            var email = _fixture.GerarEmailValido();
            AlteracaoContatoDto alteracaoContatoDto = new()
            {
                nome = string.Empty,
                telefone = string.Empty,
                email = string.Empty,
            };

            //Act
            var act = async () => await contatoAppService.AlterarContato(1, alteracaoContatoDto);

            //Assert
            await act.Should().ThrowAsync<ValidationException>();
        }

        [Fact(DisplayName = "Remover contato com sucesso.")]
        [Trait("ContatoAppService", "RemoverContato")]
        public async Task ContatoAppService_DeveRemoverContato_QuandoContatoEncontrado()
        {
            //Arrange
            var mockContatoService = new Mock<IContatoService>();
            mockContatoService
                .Setup(x => x.RemoverContato(It.IsAny<int>()));
            var contatoAppService = new ContatoAppService(mockContatoService.Object, null, null);

            //Act
            var act = async () => await contatoAppService.RemoverContato(1);

            //Assert
            await act.Should().NotThrowAsync<Exception>();
        }

        [Fact(DisplayName = "Remover contato com erro.")]
        [Trait("ContatoAppService", "RemoverContato")]
        public async Task ContatoAppService_NaoDeveRemoverContato_QuandoContatoNaoEncontrado()
        {
            //Arrange
            var mockContatoService = new Mock<IContatoService>();
            mockContatoService
                .Setup(x => x.RemoverContato(It.IsAny<int>()))
                .Returns<Exception>((ex) => throw ex);
            var contatoAppService = new ContatoAppService(mockContatoService.Object, null, null);

            //Act
            var act = async () => await contatoAppService.RemoverContato(1);

            //Assert
            await act.Should().ThrowAsync<Exception>();
        }
    }
}
