using Fiap.FCG.Game.Application.Compras.Consultar;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace Fiap.FCG.Game.WebApi.Compras.Consultar
{
    [ApiController]
    [Route("api/usuarios")]
    [ApiExplorerSettings(GroupName = "Usuário")]
    public class ConsultarBibliotecaJogosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConsultarBibliotecaJogosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("{usuarioId}/jogos")]
        [SwaggerOperation(
            Summary = "Lista jogos adquiridos por um usuário",
            Description = "Retorna os jogos comprados por um usuário com nome, valor pago e data da compra."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ListarJogosAdquiridos(int usuarioId)
        {
            var result = await _mediator.Send(new ConsultarBibliotecaQuery(usuarioId));

            if (result == null || result.Count == 0)
                return NotFound(new { sucesso = false, mensagem = "Nenhum jogo encontrado para o usuário." });

            return Ok(new
            {
                sucesso = true,
                jogos = result
            });
        }
    }
}
