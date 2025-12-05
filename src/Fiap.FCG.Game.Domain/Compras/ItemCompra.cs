namespace Fiap.FCG.Game.Domain.Compras
{
    public class ItemCompra
    {
        public int Id { get; private set; }
        public int HistoricoCompraId { get; private set; }
        public int JogoId { get; private set; }
        public decimal PrecoPago { get; private set; }

        private ItemCompra() { }

        public ItemCompra(int jogoId, decimal precoPago)
        {
            JogoId = jogoId;
            PrecoPago = precoPago;
        }
    }
}
