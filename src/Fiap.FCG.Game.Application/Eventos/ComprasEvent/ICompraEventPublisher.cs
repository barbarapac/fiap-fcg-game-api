using System.Threading.Tasks;

namespace Fiap.FCG.Game.Application.Eventos.ComprasEvent
{
    public interface ICompraEventPublisher
    {
        Task PublicarCompraRealizadaAsync(CompraRealizadaEvent evento);
    }
}
