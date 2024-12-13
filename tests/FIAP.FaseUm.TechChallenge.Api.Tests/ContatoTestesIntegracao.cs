using FIAP.FaseUm.TechChallenge.Application.Dto;
using FIAP.FaseUm.TechChallenge.Custom.Exceptions.Config;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Json;

namespace FIAP.FaseUm.TechChallenge.Api.Tests
{
    public class ContatoTestesIntegracao : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _client;

        public ContatoTestesIntegracao(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact(DisplayName = "Cadastrar contato com sucesso.")]
        [Trait("", value: "Teste Integração")]
        public async Task CadastrarContato_ComSucesso()
        {
            // Arrange
            CadastroContatoDto contato = new("Gabriel", "17992018699", "gabriel_antognoli@hotmail.com");

            // Act
            var response = await CriarContato(contato);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact(DisplayName = "Cadastrar contato com erro validação")]
        [Trait("", value: "Teste Integração")]
        public async Task CadastrarContato_ComErroValidacao()
        {
            // Arrange
            CadastroContatoDto contato = new("", "", "");

            // Act
            var response = await CriarContato(contato);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            (await response.Content.ReadFromJsonAsync<ProblemDetails>())?.Type.Should().Be(nameof(ValidationException));
        }

        [Fact(DisplayName = "Alterar contato com sucesso.")]
        [Trait("", value: "Teste Integração")]
        public async Task AlterarContato_ComSucesso()
        {
            // Arrange
            var url = $"/api/contato/1";
            AlteracaoContatoDto contato = new(1, "Gabriel Alterado", "17992028699", "gabrielantognoli2@gmail.com");

            // Act
            var response = await _client.PutAsJsonAsync(url, contato);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact(DisplayName = "Alterar contato com erro validação.")]
        [Trait("", value: "Teste Integração")]
        public async Task AlterarContato_ComErroValidacao()
        {
            // Arrange
            var url = $"/api/contato/1";
            AlteracaoContatoDto contato = new(1, "", "", "");

            // Act
            var response = await _client.PutAsJsonAsync(url, contato);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            (await response.Content.ReadFromJsonAsync<ProblemDetails>())?.Type.Should().Be(nameof(ValidationException));
        }

        [Fact(DisplayName = "Remover contato com sucesso.")]
        [Trait("", value: "Teste Integração")]
        public async Task RemoverContato_ComSucesso()
        {
            // Arrange
            var url = $"/api/contato/1";

            // Act
            var response = await _client.DeleteAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        private async Task<HttpResponseMessage> CriarContato(CadastroContatoDto contato) 
            => await _client.PostAsJsonAsync("/api/contato", contato);
    }
}
