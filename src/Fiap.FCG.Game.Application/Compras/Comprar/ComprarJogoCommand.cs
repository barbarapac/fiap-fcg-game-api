using Fiap.FCG.Game.Domain._Shared;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fiap.FCG.Game.Application.Compras.Comprar
{
    public class ComprarJogoCommand : IRequest<Result<bool>>
    {
        [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
        [SwaggerSchema("ID do Usuário que está realizando a compra")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "É necessário informar pelo menos um jogo.")]
        [MinLength(1, ErrorMessage = "Selecione pelo menos um jogo para comprar.")]
        [SwaggerSchema("Lista de IDs dos jogos que serão comprados")]
        public List<int> JogosIds { get; set; } = [];
    }
}
