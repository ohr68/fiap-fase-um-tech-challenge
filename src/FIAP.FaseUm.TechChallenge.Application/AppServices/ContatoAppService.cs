using FIAP.FaseUm.TechChallenge.Application.Dto;
using FIAP.FaseUm.TechChallenge.Application.Interfaces.AppServices;
using FIAP.FaseUm.TechChallenge.Domain.Entities;
using FIAP.FaseUm.TechChallenge.Domain.Interfaces.Services;
using FIAP.FaseUm.TechChallenge.Extensions.FluentValidation;
using FluentValidation;
using Mapster;

namespace FIAP.FaseUm.TechChallenge.Application.AppServices
{
    public class ContatoAppService(IContatoService contatoService,
        IValidator<CadastroContatoDto> validadorCadastro,
        IValidator<AlteracaoContatoDto> validadorAlteracao) : IContatoAppService
    {
        public async Task AlterarContato(int id, AlteracaoContatoDto contato)
        {
            var validacaoContato = await validadorAlteracao.ValidateAsync(contato);

            if (!validacaoContato.IsValid)
                throw new Custom.Exceptions.Config.ValidationException(validacaoContato.Errors.ObterMensagensComoStringUnica());

            await contatoService.AlterarContato(id, contato.Adapt<Contato>());
        }

        public async Task<ConsultaContatoDto> CadastrarContato(CadastroContatoDto contato)
        {
            var validacaoContato = await validadorCadastro.ValidateAsync(contato);

            if (!validacaoContato.IsValid)
                throw new Custom.Exceptions.Config.ValidationException(validacaoContato.Errors.ObterMensagensComoStringUnica());

            var contatoCadastro = contato.Adapt<Contato>();

            contatoService.CadastrarContato(contatoCadastro);

            return contatoCadastro.Adapt<ConsultaContatoDto>();
        }

        public async Task<IEnumerable<ConsultaContatoDto>> ListarContatos(string ddd) 
            => (await contatoService.ListarContatos(ddd)).Adapt<IEnumerable<ConsultaContatoDto>>();

        public Task RemoverContato(int id) 
            => contatoService.RemoverContato(id);
    }
}
