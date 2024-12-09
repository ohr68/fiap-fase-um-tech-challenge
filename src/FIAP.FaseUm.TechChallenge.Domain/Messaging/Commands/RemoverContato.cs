namespace FIAP.FaseUm.TechChallenge.Domain.Messaging.Commands;

public class RemoverContato(int id) : Message
{
    public int Id { get; private set; } = id;
}