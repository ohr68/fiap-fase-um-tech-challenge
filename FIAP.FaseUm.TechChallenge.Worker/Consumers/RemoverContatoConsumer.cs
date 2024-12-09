using FIAP.FaseUm.TechChallenge.Domain.Interfaces.Repositories;
using FIAP.FaseUm.TechChallenge.Domain.Messaging.Commands;
using MassTransit;

namespace FIAP.FaseUm.TechChallenge.Worker.Consumers;

public class RemoverContatoConsumer(ILogger<RemoverContatoConsumer> logger, IContatoRepository contatoRepository)
    : IConsumer<RemoverContato>
{
    public async Task Consume(ConsumeContext<RemoverContato> context)
    {
        try
        {
            logger.LogInformation("Processando mensagem {messageId}.", context.Message.CorrelationId);

            var contato = context.Message;

            var contatoDb = await contatoRepository.GetById(contato.Id);

            if (contatoDb is not null)
            {
                contatoRepository.Delete(contatoDb);
            }
            else
            {
                logger.LogWarning("Contato com id {id} não encontrado", contato.Id);
            }

            logger.LogInformation("Mensagem {messageId} processada com sucesso.", context.Message.CorrelationId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            throw;
        }
    }
}