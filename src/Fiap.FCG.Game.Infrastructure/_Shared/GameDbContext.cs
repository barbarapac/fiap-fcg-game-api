using Fiap.FCG.Game.Domain.Compras;
using Fiap.FCG.Game.Domain.Jogos;
using Fiap.FCG.Game.Domain.Promocoes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Fiap.FCG.Game.Infrastructure._Shared;

[ExcludeFromCodeCoverage]
public class GameDbContext : DbContext
{
    public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { }

    public DbSet<Jogo> Usuarios => Set<Jogo>();
    public DbSet<Promocao> Promocoes => Set<Promocao>();
    public DbSet<PromocaoJogo> PromocoesJogos => Set<PromocaoJogo>();
    public DbSet<HistoricoCompra> HistoricoCompras => Set<HistoricoCompra>();
    public DbSet<BibliotecaJogo> BibliotecaJogos => Set<BibliotecaJogo>();

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
