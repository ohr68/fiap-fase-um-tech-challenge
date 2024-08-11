using FIAP.FaseUm.TechChallenge.Domain.ValueObjects;

namespace FIAP.FaseUm.TechChallenge.Domain.Entities
{
    public class Contato : Entity
    {
        public string? Nome { get; private set; }
        public Telefone? Telefone { get; private set; }
        public Email? Email { get; private set; }

        protected Contato() { }

        public Contato(string nome, string telefone, string email)
        {
            if(string.IsNullOrEmpty(nome)) 
                throw new InvalidDataException("O nome informado não é válido.");

            Nome = nome;
            Telefone = new Telefone(telefone);
            Email = new Email(email);
        }

        public Contato(int id, string nome, string telefone, string email) : this(nome, telefone, email)
        {
            Id = id;
            Email = new Email(email);
        }

        public void Alterar(string nome, Telefone telefone, Email email)
        {
            Nome = nome;
            Telefone = telefone;
            Email = email;
        }
    }
}
