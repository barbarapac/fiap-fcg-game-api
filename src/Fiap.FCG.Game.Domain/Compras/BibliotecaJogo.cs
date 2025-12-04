using Fiap.FCG.Game.Domain._Shared;
using System;

namespace Fiap.FCG.Game.Domain.Compras
{
    public class BibliotecaJogo : Base
    {
        public int UsuarioId { get; private set; }
        public int JogoId { get; private set; }
        public DateTime DataAquisicao { get; private set; } = DateTime.UtcNow;

        public BibliotecaJogo(int usuarioId, int jogoId)
        {
            UsuarioId = usuarioId;
            JogoId = jogoId;
        }
    }
}
