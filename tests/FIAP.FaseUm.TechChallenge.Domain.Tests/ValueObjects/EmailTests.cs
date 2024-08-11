using FIAP.FaseUm.TechChallenge.Domain.Tests.Fixtures;
using FIAP.FaseUm.TechChallenge.Domain.ValueObjects;
using FluentAssertions;

namespace FIAP.FaseUm.TechChallenge.Domain.Tests.ValueObjects
{
    public class EmailTests : IClassFixture<EmailFixture>
    {
        private readonly EmailFixture _fixture;

        public EmailTests(EmailFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "Email válido quando endereço de e-mail está correto.")]
        [Trait("", "Email")]
        public void Email_DeveSerValido_QuandoEnderecoEstaCorreto()
        {
            // Arrange & Act
            var email = _fixture.GerarEmailValido();

            // Assert
            email.Endereco.Should().NotBeEmpty();
        }

        [Fact(DisplayName = "Email inválido quando endereço de e-mail está nulo.")]
        [Trait("", "Email")]
        public void Email_DeveSerInvalido_QuandoEnderecoEstaNulo()
        {
            // Arrange & Act
            var act = () => new Email(null);

            // Assert
            act.Should().Throw<InvalidDataException>();
        }

        [Fact(DisplayName = "Email inválido quando endereço de e-mail está vázio.")]
        [Trait("", "Email")]
        public void Email_DeveSerInvalido_QuandoEnderecoEstaVazio()
        {
            // Arrange & Act
            var act = () => new Email(string.Empty);

            // Assert
            act.Should().Throw<InvalidDataException>();
        }

        [Fact(DisplayName = "Email inválido quando endereço de e-mail menor que tamanho mínimo.")]
        [Trait("", "Email")]
        public void Email_DeveSerInvalido_QuandoEnderecoMenorQueTamanhoMinimo()
        {
            // Arrange
            var emailInvalido = new string(Enumerable.Range(0, Email.MIN_LENGTH - 1).Select(_ => 'a').ToArray());

            // Act
            var act = () => new Email(emailInvalido);

            // Assert
            act.Should().Throw<InvalidDataException>();
        }

        [Fact(DisplayName = "Email inválido quando endereço de e-mail está incorreto.")]
        [Trait("", "Email")]
        public void Email_DeveSerInvalido_QuandoEnderecoEstaIncorreto()
        {
            // Arrange & Act
            var act = () => _fixture.GenerateEmailInvalido();

            // Assert
            act.Should().Throw<InvalidDataException>();
        }
    }
}
