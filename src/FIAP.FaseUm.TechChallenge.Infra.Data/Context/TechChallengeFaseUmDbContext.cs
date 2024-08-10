using Microsoft.EntityFrameworkCore;

namespace FIAP.FaseUm.TechChallenge.Infra.Data.Context
{
    public class TechChallengeFaseUmDbContext(DbContextOptions<TechChallengeFaseUmDbContext> options)  : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TechChallengeFaseUmDbContext).Assembly);
        }
    }
}
