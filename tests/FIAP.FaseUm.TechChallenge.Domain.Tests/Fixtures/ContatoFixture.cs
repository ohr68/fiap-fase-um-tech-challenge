using Bogus;
using FIAP.FaseUm.TechChallenge.Domain.Entities;

namespace FIAP.FaseUm.TechChallenge.Domain.Tests.Fixtures
{
    public class ContatoFixture
    {
        private readonly Faker _faker;

        public ContatoFixture()
        {
            _faker = new Faker();
        }

        public Contato GerarContatoValido()
        {
            var nome = _faker.Name.FullName();
            var telefone = _faker.Phone.PhoneNumber("(##) #####-####");
            var email = _faker.Internet.Email();
            return new(nome, telefone, email);
        }

        public Contato GerarContatoInvalido()
        {
            var nome = string.Empty;
            var telefone = string.Empty; 
            var email = string.Empty;
            return new(nome, telefone, email);
        }
    }
}
