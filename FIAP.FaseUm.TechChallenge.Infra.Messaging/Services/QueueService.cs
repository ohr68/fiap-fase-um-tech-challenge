using FIAP.FaseUm.TechChallenge.Domain.Interfaces.Messaging;
using MassTransit;

namespace FIAP.FaseUm.TechChallenge.Infra.Messaging.Services;

public class QueueService(IBus bus) : IQueueService
{
    public async Task Publish(object message, CancellationToken cancellationToken = default) 
        => await bus.Publish(message, cancellationToken);

    public async Task Send(object message, CancellationToken cancellationToken = default) 
        => await bus.Send(message, cancellationToken);
}