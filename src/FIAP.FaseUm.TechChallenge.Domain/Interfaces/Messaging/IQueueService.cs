namespace FIAP.FaseUm.TechChallenge.Domain.Interfaces.Messaging;

public interface IQueueService
{
    Task Publish(object message, CancellationToken cancellationToken = default);
    Task Send(object message,  CancellationToken cancellationToken = default);
}