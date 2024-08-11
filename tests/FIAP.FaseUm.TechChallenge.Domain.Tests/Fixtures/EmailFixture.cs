using Bogus;
using FIAP.FaseUm.TechChallenge.Domain.ValueObjects;

namespace FIAP.FaseUm.TechChallenge.Domain.Tests.Fixtures
{
    public class EmailFixture
    {
        private readonly Faker _faker;

        public EmailFixture()
        {
            _faker = new Faker();
        }

        public Email GerarEmailValido() 
            => new(_faker.Internet.Email());

        public Email GenerateEmailVazio() 
            => new(string.Empty);

        public Email GenerateEmailInvalido() 
            => new(_faker.Internet.Email().Replace("@", ""));
    }
}
