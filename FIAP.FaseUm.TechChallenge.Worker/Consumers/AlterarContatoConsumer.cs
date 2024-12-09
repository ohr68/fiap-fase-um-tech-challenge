using FIAP.FaseUm.TechChallenge.Domain.Interfaces.Repositories;
using FIAP.FaseUm.TechChallenge.Domain.Messaging.Commands;
using FIAP.FaseUm.TechChallenge.Domain.ValueObjects;
using MassTransit;

namespace FIAP.FaseUm.TechChallenge.Worker.Consumers;

public class AlterarContatoConsumer(ILogger<AlterarContatoConsumer> logger, IContatoRepository contatoRepository)
    : IConsumer<AlterarContato>
{
    public async Task Consume(ConsumeContext<AlterarContato> context)
    {
        try
        {
            logger.LogInformation("Processando mensagem {messageId}.", context.Message.CorrelationId);

            var contato = context.Message;

            var contatoDb = await contatoRepository.GetById(contato.Id);

            if (contatoDb is not null)
            {
                contatoDb!.Alterar(contato.Nome, new Telefone(contato.Telefone), new Email(contato.Email));

                contatoRepository.Update(contatoDb);
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