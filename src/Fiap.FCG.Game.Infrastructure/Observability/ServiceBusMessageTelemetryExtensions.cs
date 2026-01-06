using Azure.Messaging.ServiceBus;
using System.Diagnostics;

namespace Fiap.FCG.Game.Infrastructure.Observability
{
    public static class ServiceBusMessageTelemetryExtensions
    {
        private const string CorrelationHeader = "X-Correlation-ID";

        public static void AddTelemetryContext(this ServiceBusMessage message)
        {
            var correlationId =
                Activity.Current?.GetTagItem("CorrelationId")?.ToString()
                ?? Activity.Current?.TraceId.ToString();

            if (!string.IsNullOrWhiteSpace(correlationId))
            {
                message.ApplicationProperties[CorrelationHeader] = correlationId;
            }

            if (Activity.Current != null)
            {
                message.ApplicationProperties["traceparent"] = Activity.Current.Id;
            }
        }
    }
}
