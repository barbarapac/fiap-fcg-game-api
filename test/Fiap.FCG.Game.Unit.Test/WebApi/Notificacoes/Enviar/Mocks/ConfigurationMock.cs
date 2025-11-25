using Microsoft.Extensions.Configuration;
using Moq;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Notificacoes.Enviar.Mocks;

public class ConfigurationMock : Mock<IConfiguration>
{
    public void ConfigurarIntervalo(string minutos)
    {
        Setup(c => c["ENVIA_NOTIFICACAO_INTERVALO_MINUTOS"]).Returns(minutos);
    }
}