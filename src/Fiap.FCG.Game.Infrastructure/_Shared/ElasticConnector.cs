using Fiap.FCG.Game.Application.Jogos.Consultar;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.FCG.Game.Infrastructure._Shared;

[ExcludeFromCodeCoverage]
public class ElasticConnector : IElasticConnector
{
    private readonly string _uri;
    private readonly string _apiKey;
    private readonly HttpClient _http;

    public ElasticConnector(IConfiguration config)
    {
        _uri = Environment.GetEnvironmentVariable("ELASTIC_URI") ?? config["ELASTIC_URI"];
        _apiKey = Environment.GetEnvironmentVariable("ELASTIC_API_KEY") ?? config["ELASTIC_API_KEY"];

        _http = new HttpClient();
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiKey", _apiKey);
    }

    public async Task<string> SearchAsync(string index, string queryJson)
    {
        var content = new StringContent(queryJson, Encoding.UTF8, "application/json");
        var response = await _http.PostAsync($"{_uri}/{index}/_search", content);
        return await response.Content.ReadAsStringAsync();
    }
}
