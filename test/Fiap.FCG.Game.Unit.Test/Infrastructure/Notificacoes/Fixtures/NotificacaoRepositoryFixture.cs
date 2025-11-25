using System;
using Fiap.FCG.Game.Infrastructure._Shared;
using Fiap.FCG.Game.Infrastructure.Notificacoes;
using Fiap.FCG.Game.Unit.Test._Shared;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.Notificacoes.Fixtures;

public class NotificacaoRepositoryFixture : IDisposable
{
    public GameDbContext Context { get; private set; }
    public NotificacaoRepository Repository { get; private set; }

    public NotificacaoRepositoryFixture()
    {
        Context = ContextFactory.Create();
        Repository = new NotificacaoRepository(Context);
    }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}