using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fiap.FCG.Game.Application.Observability;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        using var scope = _logger.BeginScope(BuildScope(request, requestName));

        var sw = Stopwatch.StartNew();
        _logger.LogInformation("Processamento da requisição iniciado");

        try
        {
            var response = await next();
            sw.Stop();

            Activity.Current?.AddEvent(new ActivityEvent("handler.completed"));

            _logger.LogInformation(
                "Processamento da requisição concluído. DuracaoMs: {DuracaoMs}",
                sw.ElapsedMilliseconds);

            return response;
        }
        catch (Exception ex)
        {
            sw.Stop();
            Activity.Current?.AddEvent(new ActivityEvent("handler.failed"));

            _logger.LogError(
                ex,
                "Falha no processamento da requisição. DuracaoMs: {DuracaoMs}",
                sw.ElapsedMilliseconds);

            throw;
        }
    }

    private static IReadOnlyList<KeyValuePair<string, object?>> BuildScope(TRequest request, string requestName)
    {
        var props = new List<KeyValuePair<string, object?>>
        {
            new("RequestName", requestName)
        };

        foreach (var p in request.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
        {
            if (!p.CanRead) continue;
            var name = p.Name;

            if (name.EndsWith("Id", StringComparison.OrdinalIgnoreCase) ||
                name.Equals("UsuarioId", StringComparison.OrdinalIgnoreCase) ||
                name.Equals("CompraId", StringComparison.OrdinalIgnoreCase) ||
                name.Equals("PagamentoId", StringComparison.OrdinalIgnoreCase))
            {
                props.Add(new(name, p.GetValue(request)));
                continue;
            }

            if (typeof(System.Collections.IEnumerable).IsAssignableFrom(p.PropertyType) &&
                p.PropertyType != typeof(string))
            {
                var value = p.GetValue(request) as System.Collections.IEnumerable;
                if (value != null)
                {
                    var count = 0;
                    foreach (var _ in value)
                    {
                        count++;
                        if (count > 1000) break;
                    }

                    props.Add(new(name + "Count", count));
                }
            }
        }

        return props;
    }
}
