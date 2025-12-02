using System.Diagnostics.CodeAnalysis;
using Fiap.FCG.Game.Application.Jogos.Consultar;
using Fiap.FCG.Game.Application.Notificacoes.Consultar;
using Fiap.FCG.Game.Application.Promocoes.Consultar;
using Microsoft.Extensions.DependencyInjection;

namespace Fiap.FCG.Game.Application;

[ExcludeFromCodeCoverage]
public static class Module
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblyContaining(typeof(Module)));
        
        
        services.AddScoped<IConsultaNotificacaoQuery, ConsultaNotificacaoQuery>();
        services.AddScoped<IConsultaPromocaoQuery, ConsultaPromocaoQuery>();
        services.AddScoped<IConsultaJogoQuery, ConsultaJogoQuery>();
    }
}