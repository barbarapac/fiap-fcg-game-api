using System;
using Fiap.FCG.Game.Infrastructure._Shared;
using Fiap.FCG.Game.Infrastructure.Compras;
using Fiap.FCG.Game.Unit.Test._Shared;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.Compras.Fixtures
{
    public class BibliotecaRepositoryFixture : IDisposable
    {
        public GameDbContext Context { get; private set; }
        public BibliotecaRepository Repository { get; private set; }

        public BibliotecaRepositoryFixture()
        {
            Context = ContextFactory.Create();
            Repository = new BibliotecaRepository(Context);
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}