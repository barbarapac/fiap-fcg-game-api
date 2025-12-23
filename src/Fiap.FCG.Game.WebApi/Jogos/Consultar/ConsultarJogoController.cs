using System.Text.Json;
using System.Threading.Tasks;
using Fiap.FCG.Game.Application.Jogos.Consultar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Fiap.FCG.Game.WebApi.Jogos.Consultar;

[ApiController]
[Route("api/jogos")]
[ApiExplorerSettings(GroupName = "Jogos")]
public class ConsultarJogoController : ControllerBase
{
    private readonly IConsultaJogoQuery _consulta;

    public ConsultarJogoController(IConsultaJogoQuery consulta)
    {
        _consulta = consulta;
    }

    [Authorize]
    [HttpGet("{jogoId}")]
    [SwaggerOperation(
        Summary = "Consulta um jogo por ID",
        Description = "Retorna os dados de um jogo específico."
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var jogo = await _consulta.ObterPorIdAsync(id);
        if (jogo is null)
            return NotFound(new { sucesso = false, mensagem = "Jogo não encontrado." });

        return Ok(new { sucesso = true, jogo });
    }
    
    [Authorize]
    [HttpGet]
    [SwaggerOperation(
        Summary = "Lista todos os jogos",
        Description = "Retorna todos os jogos cadastrados."
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterTodos()
    {
        var jogos = await _consulta.ObterTodosAsync();
        return Ok(new { sucesso = true, jogos });
    }


    [Authorize]
    [HttpGet("ordenados")]
    [SwaggerOperation(
        Summary = "Lista todos os jogos ordenados",
        Description = "Retorna todos os jogos cadastrados de forma ordenada."
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterPorNomeOrdenado(string nome, string tipo, bool crescente)
    {
        var jogosJson = await _consulta.ObterPorNomeOrdenadoAsync(nome, tipo, crescente);
        var jogosObj = JsonSerializer.Deserialize<object>(jogosJson);
        return Ok(new { sucesso = true, jogos = jogosObj });
    }

    [Authorize]
    [HttpGet("metricas-preco")]
    [SwaggerOperation(
        Summary = "Lista o valor médio, mínimo e máximo do preço dos jogos",
        Description = "Lista o valor médio, mínimo e máximo do preço dos jogos."
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterMetricasPreco()
    {
        var jogosJson = await _consulta.ObterMetricasPrecoAsync();
        var jogosObj = JsonSerializer.Deserialize<object>(jogosJson);
        return Ok(new { sucesso = true, jogos = jogosObj });
    }

    [Authorize]
    [HttpGet("contagem-por-tipo")]
    [SwaggerOperation(
        Summary = "Lista a quantidade de jogos por tipo de evento",
        Description = "Lista a quantidade de jogos por tipo de evento."
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterContagemPorTipo()
    {
        var jogosJson = await _consulta.ObterContagemPorTipoAsync();
        var jogosObj = JsonSerializer.Deserialize<object>(jogosJson);
        return Ok(new { sucesso = true, jogos = jogosObj });
    }

    [Authorize]
    [HttpGet("caros-ou-baratos")]
    [SwaggerOperation(
        Summary = "Lista os jogos por quantidade e ordenada",
        Description = "Lista os jogos por quantidade e ordenada (Barato ou Caro)."
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterJogosMaisCarosOuBaratos(bool maisCaros, int quantidade)
    {
        var jogosJson = await _consulta.ObterJogosMaisCarosOuBaratosAsync(maisCaros, quantidade);
        var jogosObj = JsonSerializer.Deserialize<object>(jogosJson);
        return Ok(new { sucesso = true, jogos = jogosObj });
    }

    [Authorize]
    [HttpGet("por-tipo-e-preco")]
    [SwaggerOperation(
        Summary = "Lista os jogos por quantidade e ordenada",
        Description = "Lista os jogos por quantidade e ordenada (Barato ou Caro)."
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterPorTipoEPreco(string tipo, double precoMin, double precoMax)
    {
        var jogosJson = await _consulta.ObterPorTipoEPrecoAsync(tipo, precoMin, precoMax);
        var jogosObj = JsonSerializer.Deserialize<object>(jogosJson);
        return Ok(new { sucesso = true, jogos = jogosObj });
    }
}