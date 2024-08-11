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
                .ConstructUsing(c => new ConsultaContatoDto(
                    c.Id, 
                    c.Nome!,
                    string.Format("({0}) {1}-{2}", c.Telefone!.Ddd, c.Telefone.Numero!.Substring(0, 5), c.Telefone.Numero.Substring(5)),
                    c.Email!.Endereco!));
        }
    }
}
