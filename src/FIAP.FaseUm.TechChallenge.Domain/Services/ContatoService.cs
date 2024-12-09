using FIAP.FaseUm.TechChallenge.Domain.Entities;
using FIAP.FaseUm.TechChallenge.Domain.Interfaces.Messaging;
using FIAP.FaseUm.TechChallenge.Domain.Interfaces.Repositories;
using FIAP.FaseUm.TechChallenge.Domain.Interfaces.Services;
using FIAP.FaseUm.TechChallenge.Domain.Messaging.Commands;

namespace FIAP.FaseUm.TechChallenge.Domain.Services
{
    public class ContatoService(IContatoRepository contatoRepository, IQueueService queueService) : IContatoService
    {
        public async Task AlterarContato(int id, Contato contato)
        {
            var alterarContato = new AlterarContato
            (
                id,
                contato.Nome!,
                contato.Telefone!.ToString(),
                contato.Email!.ToString()
            );

            await queueService.Publish(alterarContato);
        }

        public async Task CadastrarContato(Contato contato)
        {
            if (contato is null)
                throw new ArgumentNullException(nameof(contato));

            var criarContato = new CriarContato
            (
                contato.Nome!,
                contato.Telefone!.ToString(),
                contato.Email!.ToString()
            );

            await queueService.Publish(criarContato);
        }

        public async Task<IEnumerable<Contato>> ListarContatos(string ddd) =>
            await contatoRepository.ListarContatos(ddd) ?? new List<Contato>();

        public async Task RemoverContato(int id) => await queueService.Publish(new RemoverContato(id));
    }
}