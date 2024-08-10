using FIAP.FaseUm.TechChallenge.Domain.Entities;
using FIAP.FaseUm.TechChallenge.Domain.Interfaces.Repositories;
using FIAP.FaseUm.TechChallenge.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FIAP.FaseUm.TechChallenge.Infra.Data.Repositories
{
    public class ContatoRepository(TechChallengeFaseUmDbContext dbContext) : Repository<Contato>(dbContext), IContatoRepository
    {
        public async Task<IEnumerable<Contato>> ListarContatos(string ddd)
          => await this.entity.AsNoTracking().Where(c => c.Telefone.Ddd == ddd).ToListAsync();
    }
}
