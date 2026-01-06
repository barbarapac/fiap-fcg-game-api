using Azure.Messaging.ServiceBus;
using Fiap.FCG.Game.Infrastructure.Observability;
using System.Diagnostics;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.Observability
{
    public class ServiceBusMessageTelemetryExtensionsTests
    {
        [Fact]
        public void AddTelemetryContext_DeveAdicionarCorrelationId_PeloTagCorrelationId_QuandoExistir()
        {
            // Arrange
            using var activity = new Activity("test");
            activity.SetTag("CorrelationId", "correlation-123");
            activity.Start();

            var message = new ServiceBusMessage("payload");

            // Act
            message.AddTelemetryContext();

            // Assert
            Assert.True(message.ApplicationProperties.ContainsKey("X-Correlation-ID"));
            Assert.Equal("correlation-123", message.ApplicationProperties["X-Correlation-ID"]);
        }

        [Fact]
        public void AddTelemetryContext_DeveAdicionarCorrelationId_PeloTraceId_QuandoNaoHouverTagCorrelationId()
        {
            // Arrange
            using var activity = new Activity("test");
            activity.Start();

            var expectedTraceId = activity.TraceId.ToString();
            var message = new ServiceBusMessage("payload");

            // Act
            message.AddTelemetryContext();

            // Assert
            Assert.True(message.ApplicationProperties.ContainsKey("X-Correlation-ID"));
            Assert.Equal(expectedTraceId, message.ApplicationProperties["X-Correlation-ID"]);
        }

        [Fact]
        public void AddTelemetryContext_DeveAdicionarTraceParent_QuandoActivityExistir()
        {
            // Arrange
            using var activity = new Activity("test");
            activity.Start();

            var expectedTraceParent = activity.Id;
            var message = new ServiceBusMessage("payload");

            // Act
            message.AddTelemetryContext();

            // Assert
            Assert.True(message.ApplicationProperties.ContainsKey("traceparent"));
            Assert.Equal(expectedTraceParent, message.ApplicationProperties["traceparent"]);
        }

        [Fact]
        public void AddTelemetryContext_NaoDeveAdicionarNada_QuandoNaoExistirActivity()
        {
            // Arrange
            Activity.Current = null;
            var message = new ServiceBusMessage("payload");

            // Act
            message.AddTelemetryContext();

            // Assert
            Assert.False(message.ApplicationProperties.ContainsKey("X-Correlation-ID"));
            Assert.False(message.ApplicationProperties.ContainsKey("traceparent"));
            Assert.Empty(message.ApplicationProperties);
        }

        [Fact]
        public void AddTelemetryContext_DeveSobrescreverCorrelationId_QuandoJaExistirENaoForNulo()
        {
            // Arrange
            using var activity = new Activity("test");
            activity.SetTag("CorrelationId", "novo-correlation");
            activity.Start();

            var message = new ServiceBusMessage("payload");
            message.ApplicationProperties["X-Correlation-ID"] = "antigo-correlation";

            // Act
            message.AddTelemetryContext();

            // Assert (comportamento atual da sua extensão: sobrescreve)
            Assert.Equal("novo-correlation", message.ApplicationProperties["X-Correlation-ID"]);
        }

        [Fact]
        public void AddTelemetryContext_DeveAdicionarTraceParent_QuandoJaExistirENaoForNulo()
        {
            // Arrange
            using var activity = new Activity("test");
            activity.Start();

            var message = new ServiceBusMessage("payload");
            message.ApplicationProperties["traceparent"] = "antigo";

            // Act
            message.AddTelemetryContext();

            // Assert (comportamento atual: sobrescreve)
            Assert.Equal(activity.Id, message.ApplicationProperties["traceparent"]);
        }
    }
}
