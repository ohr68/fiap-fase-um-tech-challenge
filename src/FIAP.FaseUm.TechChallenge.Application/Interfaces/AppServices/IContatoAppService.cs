using FIAP.FaseUm.TechChallenge.Application.Dto;

namespace FIAP.FaseUm.TechChallenge.Application.Interfaces.AppServices
{
    public interface IContatoAppService
    {
        Task<IEnumerable<ConsultaContatoDto>> ListarContatos(string ddd);
        Task<ConsultaContatoDto> CadastrarContato(CadastroContatoDto contato);
        Task AlterarContato(int id, AlteracaoContatoDto contato);
        Task RemoverContato(int id);
    }
}
