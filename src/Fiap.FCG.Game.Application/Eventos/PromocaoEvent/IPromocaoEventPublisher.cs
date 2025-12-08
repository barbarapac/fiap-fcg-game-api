using System.Threading.Tasks;
using Fiap.FCG.Game.Domain.Promocoes;

namespace Fiap.FCG.Game.Application.Eventos.PromocaoEvent;

public interface IPromocaoEventPublisher
{
    Task PromocaoCadastradaPublishAsync(Promocao promocao);
    Task PromocaEditadaPublishAsync(Promocao promocao);
    Task PromocaoRemovidaPublishAsync(Promocao promocao);
}