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
            var responseContatoCriado = await CriarContato(new("Gabriel", "17992018699", "gabriel_antognoli@hotmail.com"));
            var contatoCriado = await responseContatoCriado.Content.ReadFromJsonAsync<ConsultaContatoDto>();
            var url = $"/api/contato/{contatoCriado.id}";
            AlteracaoContatoDto contato = new(contatoCriado.id, "Gabriel Alterado", "17992028699", "gabrielantognoli2@gmail.com");

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
            var responseContatoCriado = await CriarContato(new("Gabriel", "17992018699", "gabriel_antognoli@hotmail.com"));
            var contatoCriado = await responseContatoCriado.Content.ReadFromJsonAsync<ConsultaContatoDto>();
            var url = $"/api/contato/{contatoCriado.id}";
            AlteracaoContatoDto contato = new(contatoCriado.id, "", "", "");

            // Act
            var response = await _client.PutAsJsonAsync(url, contato);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            (await response.Content.ReadFromJsonAsync<ProblemDetails>())?.Type.Should().Be(nameof(ValidationException));
        }

        [Fact(DisplayName = "Alterar contato com ids diferentes.")]
        [Trait("", value: "Teste Integração")]
        public async Task AlterarContato_ComIdsDiferentes()
        {
            // Arrange
            var responseContatoCriado = await CriarContato(new("Gabriel", "17992018699", "gabriel_antognoli@hotmail.com"));
            var contatoCriado = await responseContatoCriado.Content.ReadFromJsonAsync<ConsultaContatoDto>();
            var url = $"/api/contato/{contatoCriado.id}";
            AlteracaoContatoDto contato = new(100, "", "", "");

            // Act
            var response = await _client.PutAsJsonAsync(url, contato);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact(DisplayName = "Remover contato com sucesso.")]
        [Trait("", value: "Teste Integração")]
        public async Task RemoverContato_ComSucesso()
        {
            // Arrange
            var responseContatoCriado = await CriarContato(new("Gabriel", "17992018699", "gabriel_antognoli@hotmail.com"));
            var contatoCriado = await responseContatoCriado.Content.ReadFromJsonAsync<ConsultaContatoDto>();
            var url = $"/api/contato/{contatoCriado.id}";

            // Act
            var response = await _client.DeleteAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact(DisplayName = "Remover contato com contato inexistente.")]
        [Trait("", value: "Teste Integração")]
        public async Task RemoverContato_ComContatoInexistente()
        {
            // Arrange
            var url = $"/api/contato/100";

            // Act
            var response = await _client.DeleteAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            (await response.Content.ReadFromJsonAsync<ProblemDetails>())?.Type.Should().Be(nameof(NotFoundException));
        }

        private async Task<HttpResponseMessage> CriarContato(CadastroContatoDto contato) 
            => await _client.PostAsJsonAsync("/api/contato", contato);
    }
}
