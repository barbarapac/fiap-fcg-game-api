using System;
using Fiap.FCG.Game.Unit.Test._Shared.Mocks;
using Fiap.FCG.Game.WebApi._Shared;
using Microsoft.AspNetCore.Http;

namespace Fiap.FCG.Game.Unit.Test._Shared.Fixtures;

public class ExceptionMiddlewareFixture
{
    protected LoggerMock LoggerMock { get; private set; }
    protected HostEnvironmentMock HostEnvironmentMock { get; private set; }

    protected ExceptionMiddlewareFixture()
    {
        LoggerMock = new LoggerMock();
        HostEnvironmentMock = new HostEnvironmentMock();
    }

    protected ExceptionMiddleware CriarMiddlewareQueLancaExcecao()
    {
        RequestDelegate next = _ => throw new InvalidOperationException("Erro de teste");
        return new ExceptionMiddleware(next, LoggerMock.Object, HostEnvironmentMock.Object);
    }
}