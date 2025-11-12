using Microsoft.Extensions.Hosting;
using Moq;

namespace Fiap.FCG.Game.Unit.Test._Shared.Mocks;

public class HostEnvironmentMock : Mock<IHostEnvironment>
{
    public void ConfigurarComoDesenvolvimento()
    {
        Setup(h => h.EnvironmentName).Returns(Environments.Development);
    }

    public void ConfigurarComoProducao()
    {
        Setup(h => h.EnvironmentName).Returns(Environments.Production);
    }
}