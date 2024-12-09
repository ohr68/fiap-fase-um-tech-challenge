namespace FIAP.FaseUm.TechChallenge.Domain.Messaging;

public abstract class Message
{
    public Guid CorrelationId { get; private set; } = Guid.NewGuid();
    public DateTime Timestamp { get; private set; } = DateTime.UtcNow;
}