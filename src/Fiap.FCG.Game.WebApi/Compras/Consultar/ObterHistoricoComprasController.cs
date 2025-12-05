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
    public class ObterHistoricoComprasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ObterHistoricoComprasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("{usuarioId}/compras")]
        [SwaggerOperation(
            Summary = "Lista o histórico de compras de um usuário",
            Description = "Retorna todas as compras feitas por um usuário, com os jogos adquiridos e valores pagos."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterHistorico(int usuarioId)
        {
            var result = await _mediator.Send(new ConsultarHistoricoComprasQuery(usuarioId));

            if (result is null || result.Count == 0)
                return NotFound(new { sucesso = false, mensagem = "Nenhuma compra encontrada para o usuário." });

            return Ok(new
            {
                sucesso = true,
                compras = result
            });
        }
    }
}
