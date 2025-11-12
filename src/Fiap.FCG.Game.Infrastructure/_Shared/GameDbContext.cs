using System;
using Fiap.FCG.Game.Domain.Jogos;
using Microsoft.EntityFrameworkCore;

namespace Fiap.FCG.Game.Infrastructure._Shared;

public class GameDbContext : DbContext
{
    public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { }

    public DbSet<Jogo> Usuarios => Set<Jogo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                ?.Equals("Development", StringComparison.OrdinalIgnoreCase) == true)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
        
        base.OnConfiguring(optionsBuilder);
    }

}
