namespace FIAP.FaseUm.TechChallenge.Domain.Messaging.Commands;

public class CriarContato(string nome, string telefone, string email) : Message
{
    public string Nome { get; private set; } = nome;
    public string Telefone { get; private set; } = telefone;
    public string Email { get; private set; } = email;
}