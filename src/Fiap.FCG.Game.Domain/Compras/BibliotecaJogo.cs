using Fiap.FCG.Game.Domain._Shared;
using Fiap.FCG.Game.Domain.Jogos;
using System;

namespace Fiap.FCG.Game.Domain.Compras
{
    public class BibliotecaJogo : Base
    {
        public int UsuarioId { get; private set; }
        public int JogoId { get; private set; }
        public Jogo Jogo { get; private set; }
        public DateTime DataAquisicao { get; private set; } = DateTime.UtcNow;

        private BibliotecaJogo() { }

        public BibliotecaJogo(int usuarioId, int jogoId)
        {
            UsuarioId = usuarioId;
            JogoId = jogoId;
        }
    }
}
