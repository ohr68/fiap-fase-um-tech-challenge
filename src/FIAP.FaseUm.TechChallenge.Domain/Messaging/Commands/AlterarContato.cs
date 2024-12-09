namespace FIAP.FaseUm.TechChallenge.Domain.Messaging.Commands;

public class AlterarContato(int id, string nome, string telefone, string email) : Message
{
    public int Id { get; private set; } = id;
    public string Nome { get; private set; } = nome;
    public string Telefone { get; private set; } = telefone;
    public string Email { get; private set; } = email;
}