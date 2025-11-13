using System;
using Fiap.FCG.Game.Infrastructure._Shared;
using Fiap.FCG.Game.Infrastructure.Jogos;
using Fiap.FCG.Game.Unit.Test._Shared;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.Jogos.Fixtures;

public class JogoRepositoryFixture : IDisposable
{
    public GameDbContext Context { get; private set; }
    public JogoRepository Repository { get; private set; }

    public JogoRepositoryFixture()
    {
        Context = ContextFactory.Create();
        Repository = new JogoRepository(Context);
    }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}
