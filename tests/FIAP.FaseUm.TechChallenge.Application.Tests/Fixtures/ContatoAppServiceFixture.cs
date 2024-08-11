using Bogus;
using FIAP.FaseUm.TechChallenge.Application.AppServices;
using FIAP.FaseUm.TechChallenge.Application.Dto;
using Mapster;

namespace FIAP.FaseUm.TechChallenge.Application.Tests.Fixtures
{
    public class ContatoAppServiceFixture
    {
        private readonly Faker _faker;

        public ContatoAppServiceFixture()
        {
            Setup();
            _faker = new Faker();
        }

        public CadastroContatoDto GerarCadastroContatoDtoValido()
        {
            return new()
            {
                nome = GerarNomeValido(),
                telefone = GerarTelefoneValido(),
                email = GerarEmailValido(),
            };
        }

        public string GerarNomeValido()
            => _faker.Name.FullName();

        public string GerarTelefoneValido()
            => _faker.Phone.PhoneNumber("(##) #####-####");

        public string GerarEmailValido()
            => _faker.Internet.Email().ToLower().Trim();

        public void Setup()
            => TypeAdapterConfig.GlobalSettings.Scan(typeof(ContatoAppService).Assembly);
    }
}
