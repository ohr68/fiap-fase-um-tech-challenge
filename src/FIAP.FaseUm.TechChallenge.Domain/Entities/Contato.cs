using FIAP.FaseUm.TechChallenge.Domain.ValueObjects;

namespace FIAP.FaseUm.TechChallenge.Domain.Entities
{
    public class Contato : Entity
    {
        public string Nome { get; set; }
        public Telefone Telefone { get; set; }
        public Email Email { get; set; }

        public Contato(string nome, string telefone, string email)
        {
            Nome = nome;
            Telefone = new Telefone(telefone);
            Email = new Email(email);
        }

        public Contato(int id, string nome, string telefone, string email)
        {
            Id = id;
            Nome = nome;
            Telefone = new Telefone(telefone);
            Email = new Email(email);
        }
    }
}
