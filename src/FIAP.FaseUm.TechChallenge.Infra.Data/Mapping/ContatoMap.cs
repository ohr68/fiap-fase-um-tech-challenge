using FIAP.FaseUm.TechChallenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.FaseUm.TechChallenge.Infra.Data.Mapping
{
    public class ContatoMap : IEntityTypeConfiguration<Contato>
    {
        public void Configure(EntityTypeBuilder<Contato> builder)
        {
            builder.ToTable("Contatos");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(c => c.Nome)
                .HasColumnType("VARCHAR(250)")
                .IsRequired();

            builder.OwnsOne(c => c.Telefone)
                .Property(t => t.Ddd)
                .HasColumnType("VARCHAR(5)")
                .HasColumnName("DddTelefone")
                .IsRequired();

            builder.OwnsOne(c => c.Telefone)
                .Property(t => t.Numero)
                .HasColumnType("VARCHAR(50)")
                .HasColumnName("NumeroTelefone")
                .IsRequired();

            builder.OwnsOne(c => c.Email)
                .Property(e => e.Endereco)
                .HasColumnType("VARCHAR(150)")
                .HasColumnName("Email")
                .IsRequired();
        }
    }
}
