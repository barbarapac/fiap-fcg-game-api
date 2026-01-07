using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Fiap.FCG.Game.WebApi._Shared;

public class RequestLoggingScopeMiddleware
{
    private const string CorrelationHeader = "X-Correlation-ID";
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingScopeMiddleware> _logger;

    public RequestLoggingScopeMiddleware(RequestDelegate next, ILogger<RequestLoggingScopeMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var correlationId =
            context.Request.Headers.TryGetValue(CorrelationHeader, out var headerValue)
            && !string.IsNullOrWhiteSpace(headerValue)
                ? headerValue.ToString()
                : (Activity.Current?.TraceId.ToString() ?? context.TraceIdentifier);

        Activity.Current?.SetTag("CorrelationId", correlationId);

        var activity = Activity.Current;

        context.Response.OnStarting(() =>
        {
            context.Response.Headers[CorrelationHeader] = correlationId;
            return Task.CompletedTask;
        });

        using var scope = _logger.BeginScope(new[]
        {
        new KeyValuePair<string, object?>("CorrelationId", correlationId),
        new KeyValuePair<string, object?>("TraceId", activity?.TraceId.ToString()),
        new KeyValuePair<string, object?>("SpanId", activity?.SpanId.ToString()),
        new KeyValuePair<string, object?>("HttpMethod", context.Request.Method),
        new KeyValuePair<string, object?>("HttpPath", context.Request.Path.ToString())
    });

        var sw = Stopwatch.StartNew();
        _logger.LogInformation("Requisição HTTP iniciada");

        try
        {
            await _next(context);
            sw.Stop();

            _logger.LogInformation(
                "Requisição HTTP finalizada. StatusCode: {StatusCode}. DuracaoMs: {DuracaoMs}",
                context.Response.StatusCode,
                sw.ElapsedMilliseconds);
        }
        catch (Exception ex)
        {
            sw.Stop();

            _logger.LogError(
                ex,
                "Falha ao processar requisição HTTP. DuracaoMs: {DuracaoMs}",
                sw.ElapsedMilliseconds);

            throw;
        }
    }
}
