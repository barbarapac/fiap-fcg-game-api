using Fiap.FCG.Game.Application.Integracoes;
using Fiap.FCG.User.WebApi.Protos;
using System.Threading.Tasks;

namespace Fiap.FCG.Game.Infrastructure.Integracoes
{
    public class UsuarioGrpcIntegrationService : IUsuarioIntegrationService
    {
        private readonly UsuarioService.UsuarioServiceClient _client;

        public UsuarioGrpcIntegrationService(UsuarioService.UsuarioServiceClient client)
        {
            _client = client;
        }

        public async Task<bool> UsuarioExiste(int usuarioId)
        {
            var response = await _client.ObterUsuarioPorIdAsync(
                new ObterUsuarioPorIdRequest { UsuarioId = usuarioId }
            );

            return response.Existe;
        }
    }

}
