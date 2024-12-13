using FIAP.FaseUm.TechChallenge.Domain.Entities;
using FIAP.FaseUm.TechChallenge.Domain.Interfaces.Repositories;
using FIAP.FaseUm.TechChallenge.Domain.Messaging.Commands;
using MassTransit;

namespace FIAP.FaseUm.TechChallenge.Worker.Consumers;

public class CriarContatoConsumer(ILogger<CriarContatoConsumer> logger, IContatoRepository contatoRepository)
    : IConsumer<CriarContato>
{
    public Task Consume(ConsumeContext<CriarContato> context)
    {
        try
        {
            logger.LogInformation("Processando mensagem {messageId}.", context.Message.CorrelationId);

            var contato = context.Message;

            contatoRepository.Add(new Contato(contato.Nome, contato.Telefone, contato.Email));

            logger.LogInformation("Mensagem {messageId} processada com sucesso.", context.Message.CorrelationId);

            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            throw;
        }
    }
}