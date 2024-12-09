using FIAP.FaseUm.TechChallenge.Domain.Entities;

namespace FIAP.FaseUm.TechChallenge.Domain.Interfaces.Services
{
    public interface IContatoService
    {
        Task<IEnumerable<Contato>> ListarContatos(string ddd);
        Task CadastrarContato(Contato contato);
        Task AlterarContato(int id, Contato contato);
        Task RemoverContato(int id);
    }
}
