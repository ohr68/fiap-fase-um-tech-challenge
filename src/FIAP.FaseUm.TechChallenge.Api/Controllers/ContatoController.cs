using FIAP.FaseUm.TechChallenge.Application.Dto;
using FIAP.FaseUm.TechChallenge.Application.Interfaces.AppServices;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.FaseUm.TechChallenge.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController(IContatoAppService contatoAppService) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> ListarContatos(string? ddd) => Ok(await contatoAppService.ListarContatos(ddd));


        [HttpPost]
        public async Task<IActionResult> CadastrarContato([FromBody] CadastroContatoDto contato)
        {
            var contatoCadastrado = await contatoAppService.CadastrarContato(contato);
            var urlContatoCadastrado = Url.Action(nameof(CadastrarContato), new { id = contatoCadastrado.id }) ?? $"/{contatoCadastrado.id}";

            return Created(urlContatoCadastrado, contatoCadastrado);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> AlterarContato(int id, [FromBody] AlteracaoContatoDto contato)
        {
            if (id == 0)
                return BadRequest("Informe um id válido.");

            if (id != contato.id)
                return BadRequest("O id informado é diferente do que está sendo alterado.");

            await contatoAppService.AlterarContato(id, contato);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoverContato(int id)
        {
            if (id == 0)
                return BadRequest("Informe um id válido.");

            await contatoAppService.RemoverContato(id);

            return NoContent();
        }
    }
}
