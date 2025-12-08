using Fiap.FCG.Game.Domain._Shared;
using Fiap.FCG.Game.Domain.Jogos;

namespace Fiap.FCG.Game.Domain.Compras
{
    public class ItemCompra : Base
    {
        public int HistoricoCompraId { get; set; }
        public HistoricoCompra HistoricoCompra { get; set; }
        public int JogoId { get; set; }
        public Jogo Jogo { get; set; }
        public decimal PrecoPago { get; set; }

        public ItemCompra(int jogoId, decimal precoPago)
        {
            JogoId = jogoId;
            PrecoPago = precoPago;
        }
    }
}
