using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fiap.FCG.Game.Application.Notificacoes.Enviar;
using Fiap.FCG.Game.Domain.Notificacoes;
using Fiap.FCG.Game.Unit.Test.Application.Notificacoes.Enviar.Fakers;
using Fiap.FCG.Game.Unit.Test.Application.Notificacoes.Enviar.Fixtures;
using Fiap.FCG.Game.Unit.Test.Application.Notificacoes.Enviar.Mocks;
using Moq;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.Application.Notificacoes.Enviar;

public class EnviarNotificacaoHandlerTest : EnviarNotificacaoHandlerFixture
{
    [Fact]
    public async Task Handle_QuandoNaoExistemUsuariosNotificaveis_NaoDeveExecutarNenhumaAcao()
    {
        // Arrange
        UsuarioGatewayMock.ConfigurarUsuariosNotificaveis(UsuarioNotificavelFaker.ListaVazia());

        // Act
        await Handler.Handle(new EnviarNotificacaoCommand(), CancellationToken.None);

        // Assert
        PromocaoRepositoryMock.VerifyNoOtherCalls();
        NotificacaoRepositoryMock.VerifyNoOtherCalls();
        EmailSenderMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task Handle_QuandoNaoExistemPromocoes_NaoDeveNotificar()
    {
        // Arrange
        UsuarioGatewayMock.ConfigurarUsuariosNotificaveis(UsuarioNotificavelFaker.ListaValida());
        PromocaoRepositoryMock.ConfigurarPromocoesAtivas(PromocaoFaker.ListaVazia());

        // Act
        await Handler.Handle(new EnviarNotificacaoCommand(), CancellationToken.None);

        // Assert
        NotificacaoRepositoryMock.Verify(x => x.Adicionar(It.IsAny<Notificacao>()), Times.Never);
        EmailSenderMock.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task Handle_QuandoHaPromocoesEUsuarios_DeveEnviarNotificacoesESalvar()
    {
        // Arrange
        var usuarios = UsuarioNotificavelFaker.ListaValida();
        UsuarioGatewayMock.ConfigurarUsuariosNotificaveis(usuarios);

        var promocoes = PromocaoFaker.ListaValidaComJogos(2);
        PromocaoRepositoryMock.ConfigurarPromocoesAtivas(promocoes);

        NotificacaoRepositoryMock.ConfigurarUsuariosNaoNotificados(new List<int> { usuarios.First().Id });

        // Act
        await Handler.Handle(new EnviarNotificacaoCommand(), CancellationToken.None);

        // Assert
        // EmailSenderMock.GarantirEmailEnviado(usuarios.First().Email);
        NotificacaoRepositoryMock.GarantirAdicionarFoiChamado();
        NotificacaoRepositoryMock.GarantirSaveChangesChamado();
    }
}
