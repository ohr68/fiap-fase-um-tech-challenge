using FIAP.FaseUm.TechChallenge.Application.Dto;
using FIAP.FaseUm.TechChallenge.Domain.Entities;
using Mapster;

namespace FIAP.FaseUm.TechChallenge.Application.Mapping
{
    public class ContatoMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CadastroContatoDto, Contato>()
                .ConstructUsing(c => new Contato(c.nome, c.telefone, c.email));

            config.NewConfig<AlteracaoContatoDto, Contato>()
                .ConstructUsing(c => new Contato(c.id, c.nome, c.telefone, c.email));

            config.NewConfig<Contato, ConsultaContatoDto>()
                .ConstructUsing(c => new ConsultaContatoDto(c.Id, c.Nome, $"({c.Telefone.Ddd}){c.Telefone.Numero}", c.Email.Endereco));
        }
    }
}
