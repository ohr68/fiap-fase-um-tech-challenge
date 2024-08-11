using FIAP.FaseUm.TechChallenge.Domain.Tests.Fixtures;
using FluentAssertions;

namespace FIAP.FaseUm.TechChallenge.Domain.Tests.Entities
{
    public class ContatoTests : IClassFixture<ContatoFixture>
    {
        private readonly ContatoFixture _fixture;

        public ContatoTests(ContatoFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "Contato deve ser válido quando dados do contato são válidos.")]
        [Trait("", "Contato")]
        public void Contato_DeveSerValido_QuandoDadosCorretos()
        {
            // Arrange & Act
            var contato = _fixture.GerarContatoValido();

            // Assert
            contato.Nome.Should().NotBeEmpty();
            contato.Telefone.Should().NotBeNull();
            contato.Email.Should().NotBeNull();
        }

        [Fact(DisplayName = "Contato deve ser inválido quando dados do contato são inválidos.")]
        [Trait("", "Contato")]
        public void Contato_DeveSerInvalido_QuandoDadosIncorretos()
        {
            // Arrange & Act
            var act = () => _fixture.GerarContatoInvalido();

            // Assert
            act.Should().Throw<InvalidDataException>();
        }
    }
}
