using System.Threading.Tasks;
using Fiap.FCG.Game.Application.Eventos.PromocaoEvent;
using Fiap.FCG.Game.Domain.Promocoes;
using Moq;

namespace Fiap.FCG.Game.Unit.Test.Application.Promocoes.Cadastrar.Mocks;

public class PromocaoEventPublisherMock : Mock<IPromocaoEventPublisher>
{
    public void ConfigurarPromocaoCadastradaPublishAsync()
    {
        Setup(p => p.PromocaoCadastradaPublishAsync(It.IsAny<Promocao>()))
            .Returns(Task.CompletedTask);
    }

    public void GarantirPromocaoCadastradaPublishAsyncChamado()
    {
        Verify(p => p.PromocaoCadastradaPublishAsync(It.IsAny<Promocao>()), Times.Once);
    }

    public void GarantirPromocaoCadastradaPublishAsyncNaoChamado()
    {
        Verify(p => p.PromocaoCadastradaPublishAsync(It.IsAny<Promocao>()), Times.Never);
    }
}