using System.Threading.Tasks;
using Fiap.FCG.Game.Application.Promocoes.Cadastar;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Fiap.FCG.Game.WebApi.Promocoes.Cadastrar;

[ApiController]
[Route("api/promocoes")]
[ApiExplorerSettings(GroupName = "Promoção")]
public class CadastrarPromocaoController : ControllerBase
{
    private readonly IMediator _mediator;

    public CadastrarPromocaoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [SwaggerOperation(
        Summary = "Cadastra uma nova promoção",
        Description = "Realiza o cadastro de uma nova promoção de jogo."
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Cadastrar([FromBody] CadastrarPromocaoCommand command)
    {
        var result = await _mediator.Send(command);

        var response = new
        {
            sucesso  = result.Sucesso,
            mensagem = result.Sucesso ? "Promoção cadastrada com sucesso." : result.Erro,
            valor    = result.Sucesso ? result.Valor : null
        };

        return result.Sucesso ? Ok(response) : BadRequest(response);
    }
}