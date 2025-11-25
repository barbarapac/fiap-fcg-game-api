using System;
using Fiap.FCG.Game.Application;
using Fiap.FCG.Game.Infrastructure;
using Fiap.FCG.Game.WebApi;
using Fiap.FCG.Game.WebApi._Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Fiap.FCG.User.WebApi.Protos; 

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();
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

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); 
app.Run();