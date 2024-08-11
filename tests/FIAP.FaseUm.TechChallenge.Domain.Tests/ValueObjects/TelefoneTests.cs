using FIAP.FaseUm.TechChallenge.Domain.Tests.Fixtures;
using FIAP.FaseUm.TechChallenge.Domain.ValueObjects;
using FluentAssertions;

namespace FIAP.FaseUm.TechChallenge.Domain.Tests.ValueObjects
{
    public class TelefoneTests : IClassFixture<TelefoneFixture>
    {
        private readonly TelefoneFixture _fixture;

        public TelefoneTests(TelefoneFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "Telefone válido quando número de telefone está correto.")]
        [Trait("", "Telefone")]
        public void Telefone_DeveSerValido_QuandoNumeroEstaCorreto()
        {
            // Arrange & Act
            var telefone = _fixture.GerarTelefoneValido();

            // Assert
            telefone.Numero.Should().NotBeEmpty();
        }

        [Fact(DisplayName = "Telefone invalido quando número de telefone está vazio.")]
        [Trait("", "Telefone")]
        public void Telefone_DeveSerInvalido_QuandoNumeroEstaVazio()
        {
            // Arrange & Act
            var act = () => _fixture.GerarTelefoneVazio();

            // Assert
            act.Should().Throw<InvalidDataException>();
        }

        [Fact(DisplayName = "Telefone invalido quando número de telefone está nulo.")]
        [Trait("", "Telefone")]
        public void Telefone_DeveSerInvalido_QuandoNumeroEstaNulo()
        {
            // Arrange & Act
            var act = () => new Telefone(null);

            // Assert
            act.Should().Throw<InvalidDataException>();
        }

        [Fact(DisplayName = "Telefone invalido quando número de telefone está diferente do tamanho permitido.")]
        [Trait("", "Telefone")]
        public void Telefone_DeveSerInvalido_QuandoNumeroDiferenteTamanhoPermitido()
        {
            // Arrange 
            var telefoneInvalido = new string(Enumerable.Range(0, Telefone.LENGTH + 2).Select(_ => '1').ToArray());

            var act = () => new Email(telefoneInvalido);

            // Assert
            act.Should().Throw<InvalidDataException>();
        }

        [Fact(DisplayName = "Telefone invalido quando número de telefone está incorreto.")]
        [Trait("", "Telefone")]
        public void Telefone_DeveSerInvalido_QuandoNumeroEstaIncorreto()
        {
            // Arrange & Act
            var act = () => _fixture.GerarTelefoneInvalido();

            // Assert
            act.Should().Throw<InvalidDataException>();
        }
    }
}
