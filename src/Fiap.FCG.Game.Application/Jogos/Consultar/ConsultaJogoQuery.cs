using Fiap.FCG.Game.Domain.Jogos;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Fiap.FCG.Game.Application.Jogos.Consultar;

[ExcludeFromCodeCoverage]
public class ConsultaJogoQuery : IConsultaJogoQuery
{
    private readonly IJogoRepository _repository;
    private readonly IElasticConnector _elastic;
    private const string IndexName = "games";

    public ConsultaJogoQuery(IJogoRepository repository, IElasticConnector elastic)
    {
        _repository = repository;
        _elastic = elastic;
    }

    public async Task<List<JogoResponse>> ObterTodosAsync()
    {
        var jogos = await _repository.ObterTodosAsync();
        return jogos.Select(Mapear).ToList();
    }

    public async Task<JogoResponse?> ObterPorIdAsync(int id)
    {
        var jogo = await _repository.ObterPorIdAsync(id);
        return jogo is null ? null : Mapear(jogo);
    }

    // Buscar por Nome, Tipo e Ordenar por Preço
    public async Task<string> ObterPorNomeOrdenadoAsync(string nome, string tipo, bool crescente)
    {
        var order = crescente ? "asc" : "desc";
        var queryJson = $@"{{
                ""query"": {{
                    ""bool"": {{
                        ""must"": [
                            {{ ""match"": {{ ""Nome"": ""{nome}"" }} }}
                        ]
                    }}
                }},
                ""sort"": [{{ ""Preco"": {{ ""order"": ""{order}"" }} }}]
            }}";

        return await _elastic.SearchAsync(IndexName, queryJson);
    }

    //Média, Mínimo e Máximo de Preço dos Jogos
    public async Task<string> ObterMetricasPrecoAsync()
    {
        var queryJson = @"{
                ""size"": 0,
                ""aggs"": {
                    ""preco_medio"": { ""avg"": { ""field"": ""Preco"" } },
                    ""preco_min"": { ""min"": { ""field"": ""Preco"" } },
                    ""preco_max"": { ""max"": { ""field"": ""Preco"" } }
                }
            }";

        return await _elastic.SearchAsync(IndexName, queryJson);
    }

    //Contagem de Jogos por Tipo
    public async Task<string> ObterContagemPorTipoAsync()
    {
        var queryJson = @"{
                ""size"": 0,
                ""aggs"": {
                    ""por_tipo"": {
                        ""terms"": { ""field"": ""Tipo.keyword"" }
                    }
                }
            }";

        return await _elastic.SearchAsync(IndexName, queryJson);
    }

    //Jogos Mais Caros/Baratos
    public async Task<string> ObterJogosMaisCarosOuBaratosAsync(bool maisCaros, int quantidade)
    {
        var order = maisCaros ? "desc" : "asc";
        var queryJson = $@"{{
                ""size"": {quantidade},
                ""sort"": [{{ ""Preco"": {{ ""order"": ""{order}"" }} }}]
            }}";

        return await _elastic.SearchAsync(IndexName, queryJson);
    }

    public async Task<string> ObterPorTipoEPrecoAsync(string tipo, double precoMin, double precoMax)
    {
        var queryJson = $@"{{
                ""query"": {{
                    ""bool"": {{
                        ""must"": [
                            {{ ""match"": {{ ""Tipo"": ""{tipo}"" }} }},
                            {{ ""range"": {{ ""Preco"": {{ ""gte"": {precoMin}, ""lte"": {precoMax} }} }} }}
                        ]
                    }}
                }}
            }}";

        return await _elastic.SearchAsync(IndexName, queryJson);
    }

    private static JogoResponse Mapear(Jogo jogo)
    {
        return new JogoResponse
        {
            Id = jogo.Id,
            Nome = jogo.Nome,
            Preco = jogo.Preco
        };
    }
}
