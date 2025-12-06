using System.Threading.Tasks;
using Fiap.FCG.Game.Domain.Jogos;

namespace Fiap.FCG.Game.Application.Eventos.GameEvent;

public interface IGameEventPublisher
{
    Task JogoCadastradoPublishAsync(Jogo jogo);
    Task JogoEditadoPublishAsync(Jogo jogo);
}