using Azure.Monitor.OpenTelemetry.Exporter;
using Fiap.FCG.Game.Application;
using Fiap.FCG.Game.Infrastructure;
using Fiap.FCG.Game.WebApi;
using Fiap.FCG.Game.WebApi._Shared;
using Fiap.FCG.User.WebApi.Protos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();
var serviceName = "fcg-game-api";

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddOpenTelemetry(o =>
{
    o.IncludeScopes = true;
    o.ParseStateValues = true;
    o.IncludeFormattedMessage = true;
});

var appInsightsConnection =
    builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]
    ?? Environment.GetEnvironmentVariable("APPLICATIONINSIGHTS_CONNECTION_STRING");

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource =>
        resource.AddService(serviceName))
    .WithTracing(tracing =>
    {
        tracing
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddEntityFrameworkCoreInstrumentation();

        if (!string.IsNullOrWhiteSpace(appInsightsConnection))
        {
            tracing.AddAzureMonitorTraceExporter();
        }
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations(); 
});

builder.Services.AddControllers(); 
builder.Services.AddInfrastructure(builder.Configuration); 
builder.Services.AddApplication(); 
builder.Services.AddWebApi();

builder.Services.AddGrpcClient<UsuarioService.UsuarioServiceClient>(o =>
{
    var url= Environment.GetEnvironmentVariable("URI_USUARIO_API") ?? builder.Configuration["URI_USUARIO_API"];
    o.Address = new Uri(url);
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseMiddleware<RequestLoggingScopeMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); 
app.Run();