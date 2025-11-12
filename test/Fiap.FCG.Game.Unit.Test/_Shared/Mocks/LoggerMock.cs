using Fiap.FCG.Game.WebApi._Shared;
using Microsoft.Extensions.Logging;
using Moq;

namespace Fiap.FCG.Game.Unit.Test._Shared.Mocks;

public class LoggerMock : Mock<ILogger<ExceptionMiddleware>> { }