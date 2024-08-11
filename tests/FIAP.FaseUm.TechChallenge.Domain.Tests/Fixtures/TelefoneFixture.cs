using Bogus;
using FIAP.FaseUm.TechChallenge.Domain.ValueObjects;

namespace FIAP.FaseUm.TechChallenge.Domain.Tests.Fixtures
{
    public class TelefoneFixture
    {
        private readonly Faker _faker;

        public TelefoneFixture()
        {
            _faker = new Faker();
        }

        public Telefone GerarTelefoneValido()
            => new(_faker.Phone.PhoneNumber("(##) #####-####"));

        public Telefone GerarTelefoneVazio()
           => new(string.Empty);

        public Telefone GerarTelefoneInvalido()
           => new(_faker.Phone.PhoneNumber("(##) ####-####"));
    }
}
