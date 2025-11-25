using System;
using Fiap.FCG.Game.Infrastructure._Shared;
using Fiap.FCG.Game.Infrastructure.Promocoes;
using Fiap.FCG.Game.Unit.Test._Shared;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.Promocoes.Fixtures;

public class PromocaoRepositoryFixture : IDisposable
{
    public GameDbContext Context { get; private set; }
    public PromocaoRepository Repository { get; private set; }

    public PromocaoRepositoryFixture()
    {
        Context    = ContextFactory.Create();
        Repository = new PromocaoRepository(Context);
    }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}