using FIAP.FaseUm.TechChallenge.Domain.Entities;
using FIAP.FaseUm.TechChallenge.Domain.Interfaces.Repositories;
using FIAP.FaseUm.TechChallenge.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FIAP.FaseUm.TechChallenge.Infra.Data.Repositories
{
    public class Repository<T>(TechChallengeFaseUmDbContext dbContext) : IRepository<T> where T : Entity
    {
        public DbSet<T> entity => dbContext.Set<T>();

        public void Add(T entity) => this.entity.Add(entity);

        public void Delete(T entity) => this.entity.Remove(entity);

        public async Task<IEnumerable<T>> GetAll() => await this.entity.AsNoTracking().ToListAsync();

        public async Task<T?> GetById(int id) => await this.entity.FirstOrDefaultAsync(x => x.Id == id);

        public void Update(T entity) => this.entity.Update(entity);       
    }
}
