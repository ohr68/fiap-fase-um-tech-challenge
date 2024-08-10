using FIAP.FaseUm.TechChallenge.Domain.Entities;

namespace FIAP.FaseUm.TechChallenge.Domain.Interfaces.Repositories
{
    public interface IContatoRepository : IRepository<Contato>
    {
        Task<IEnumerable<Contato>> ListarContatos(string ddd);
    }
}
