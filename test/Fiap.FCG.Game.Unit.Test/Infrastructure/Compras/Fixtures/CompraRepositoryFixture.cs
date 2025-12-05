using System;
using Fiap.FCG.Game.Infrastructure._Shared;
using Fiap.FCG.Game.Infrastructure.Compras;
using Fiap.FCG.Game.Unit.Test._Shared;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.Compras.Fixtures
{
    public class CompraRepositoryFixture : IDisposable
    {
        public GameDbContext Context { get; }
        public CompraRepository Repository { get; }

        public CompraRepositoryFixture()
        {
            Context = ContextFactory.Create();
            Repository = new CompraRepository(Context);
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}