using System;
using Fiap.FCG.Game.Infrastructure._Shared;
using Fiap.FCG.Game.Infrastructure.Compras;
using Fiap.FCG.Game.Unit.Test._Shared;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.Compras.Fixtures
{
    public class HistoricoCompraRepositoryFixture : IDisposable
    {
        public GameDbContext Context { get; }
        public HistoricoCompraRepository Repository { get; }

        public HistoricoCompraRepositoryFixture()
        {
            Context = ContextFactory.Create();
            Repository = new HistoricoCompraRepository(Context);
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}