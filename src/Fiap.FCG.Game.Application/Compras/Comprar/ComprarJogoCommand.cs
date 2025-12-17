using Fiap.FCG.Game.Application.Eventos.ComprasEvent;
using Fiap.FCG.Game.Domain._Shared;
using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fiap.FCG.Game.Application.Compras.Comprar
{
    public class ComprarJogoCommand : IRequest<Result<bool>>
    {
        [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "É necessário informar pelo menos um jogo.")]
        [MinLength(1)]
        public List<int> JogosIds { get; set; } = [];

        [Required(ErrorMessage = "O método de pagamento é obrigatório.")]
        public EMetodoPagamento MetodoPagamento { get; set; }

        public EBandeiraCartao? BandeiraCartao { get; set; }
    }
}
