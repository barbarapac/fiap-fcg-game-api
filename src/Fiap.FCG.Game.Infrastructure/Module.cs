using Fiap.FCG.Game.Application.Eventos.ComprasEvent;
using Fiap.FCG.Game.Application.Eventos.GameEvent;
using Fiap.FCG.Game.Application.Eventos.PromocaoEvent;
using Fiap.FCG.Game.Application.Jogos.Consultar;
using Fiap.FCG.Game.Application.Notificacoes.Consultar;
using Fiap.FCG.Game.Application.Notificacoes.Enviar;
using Fiap.FCG.Game.Domain.Compras;
using Fiap.FCG.Game.Domain.Jogos;
using Fiap.FCG.Game.Domain.Notificacoes;
using Fiap.FCG.Game.Domain.Promocoes;
using Fiap.FCG.Game.Infrastructure._Shared;
using Fiap.FCG.Game.Infrastructure.Compras;
using Fiap.FCG.Game.Infrastructure.Jogos;
using Fiap.FCG.Game.Infrastructure.Notificacoes;
using Fiap.FCG.Game.Infrastructure.Promocoes;
using Fiap.FCG.Game.Infrastructure.PublisherEvent.ComprasEvent;
using Fiap.FCG.Game.Infrastructure.PublisherEvent.GameEvent;
using Fiap.FCG.Game.Infrastructure.PublisherEvent.PromocaoEvent;
using Fiap.FCG.Game.Infrastructure.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Fiap.FCG.Game.Infrastructure;

[ExcludeFromCodeCoverage]
public static class Module
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
        AddAuthentication(services, configuration);
        AddServices(services);
        AddPublishers(services);
    }

    private static void AddPublishers(IServiceCollection services)
    {
        services.AddScoped<IPromocaoEventPublisher, PromocaoEventPublisher>();
        services.AddScoped<IGameEventPublisher, GameEventPublisher>();
        services.AddScoped<ICompraEventPublisher, CompraEventPublisher>();
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<IUsuarioNotificationGateway, UsuarioNotificationGateway>();
        services.AddScoped<IElasticConnector, ElasticConnector>();
    }

    private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        var fromEnvJwtIssuer   = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? configuration["JWT_ISSUER"];
        var fromEnvJwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? configuration["JWT_AUDIENCE"];
        var fromEnvJwtKey      = Environment.GetEnvironmentVariable("JWT_KEY") ?? configuration["JWT_KEY"];
        
        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer   = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer      = fromEnvJwtIssuer,
                    ValidAudience    = fromEnvJwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(fromEnvJwtKey!))
                };
            });

        services.AddAuthorization();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var fromEnv    = Environment.GetEnvironmentVariable("GAME_CONNECTION_STRING");
        var fromConfig = configuration["GAME_CONNECTION_STRING"];
        var connectionString = !string.IsNullOrWhiteSpace(fromEnv) ? fromEnv : fromConfig;

        services.AddDbContext<GameDbContext>(options =>
            options.UseNpgsql(connectionString));
    }
    
    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IJogoRepository, JogoRepository>();
        services.AddScoped<IPromocaoRepository, PromocaoRepository>();
        services.AddScoped<INotificacaoRepository, NotificacaoRepository>();
        services.AddScoped<ICompraRepository, CompraRepository>();
        services.AddScoped<IHistoricoCompraRepository, HistoricoCompraRepository>();
        services.AddScoped<IBibliotecaRepository, BibliotecaRepository>();
    }
}