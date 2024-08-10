using FIAP.FaseUm.TechChallenge.Domain.Entities;

namespace FIAP.FaseUm.TechChallenge.Domain.Interfaces.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        public void Add(T entity);
        public void Delete(T entity);
        public void Update(T entity);
        public Task<IEnumerable<T>> GetAll();
        public Task<T> GetById(int id);
    }
}
