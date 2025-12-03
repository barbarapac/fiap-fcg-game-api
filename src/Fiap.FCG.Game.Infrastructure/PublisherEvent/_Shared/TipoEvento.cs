using System.Text.Json.Serialization;

namespace Fiap.FCG.Game.Infrastructure.PublisherEvent._Shared;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TipoEvento
{
    JOGO_CADASTADO,
    JOGO_ATUALIZADO,
    PROMOCAO_CADASTRADA,
    PROMOCAO_ATUALIZADA,
    PROMOCAO_DELETADA,
}