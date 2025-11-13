using System;
using Fiap.FCG.Game.Infrastructure._Shared;
using Microsoft.EntityFrameworkCore;

namespace Fiap.FCG.Game.Unit.Test._Shared;

public static class ContextFactory
{
    public static GameDbContext Create()
    {
        var options = new DbContextOptionsBuilder<GameDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) 
            .Options;

        return new GameDbContext(options);
    }
}