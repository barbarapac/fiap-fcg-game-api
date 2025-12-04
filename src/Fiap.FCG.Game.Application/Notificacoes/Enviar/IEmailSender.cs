using System.Threading.Tasks;

namespace Fiap.FCG.Game.Application.Notificacoes.Enviar
{
    public interface IEmailSender
    {
        Task EnviarAsync(string destino, string assunto, string corpo);
    }
}
