using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Fiap.FCG.Game.Application.Notificacoes.Consultar;
using Fiap.FCG.User.WebApi.Protos;
using Microsoft.Extensions.Configuration;

namespace Fiap.FCG.Game.Infrastructure.Usuarios;

public class UsuarioNotificationGateway : IUsuarioNotificationGateway
{
    private readonly UsuarioService.UsuarioServiceClient _client;
    private readonly HttpClient _httpClient;

    public UsuarioNotificationGateway(UsuarioService.UsuarioServiceClient client, HttpClient httpClient, IConfiguration config)
    {
        _client = client;
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("URI_USUARIO_API") ?? config["URI_USUARIO_API"]);
    }

    public async Task<IList<UsuarioNotificavelDto>> ObterUsuariosNotificaveisGrpcAsync(
        CancellationToken cancellationToken = default)
    {
        var request = new ObterUsuariosComNotificacoesRequest();

        var response = await _client.ObterUsuariosComNotificacoesAsync(
            request,
            cancellationToken: cancellationToken);

        return response.Usuarios
            .Select(u => new UsuarioNotificavelDto
            {
                Id = u.Id,
                Nome = u.Nome,
                Email = u.Email
            })
            .ToList();
    }

    public async Task<IList<UsuarioNotificavelDto>> ObterUsuariosNotificaveisHttpAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync("/api/usuarios/com-notificacoes", cancellationToken);

        response.EnsureSuccessStatusCode();

        var usuarios = await response.Content.ReadFromJsonAsync<IList<UsuarioNotificavelDto>>(cancellationToken: cancellationToken);

        return usuarios ?? new List<UsuarioNotificavelDto>();
    }

}