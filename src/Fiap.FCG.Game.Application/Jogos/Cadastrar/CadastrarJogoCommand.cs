using System.ComponentModel.DataAnnotations;
using Fiap.FCG.Game.Domain._Shared;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Fiap.FCG.Game.Application.Jogos.Cadastrar
{
    public class CadastrarJogoCommand : IRequest<Result<string>>
    {
        [Required(ErrorMessage = "Nome do Jogo é obrigatório.")]
        [StringLength(100, ErrorMessage = "Nome deve conter no máximo {1} caracteres.")]
        [SwaggerSchema("Nome do Jogo")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Preço do Jogo é obrigatório.")]
        [DataType(DataType.Currency)]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        [SwaggerSchema("Preço do Jogo")]
        public decimal Preco { get; set; }
    }
}
