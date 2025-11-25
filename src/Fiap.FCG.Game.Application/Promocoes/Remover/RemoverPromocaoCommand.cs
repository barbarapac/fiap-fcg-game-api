using System.ComponentModel.DataAnnotations;
using Fiap.FCG.Game.Domain._Shared;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Fiap.FCG.Game.Application.Promocoes.Remover;

public class RemoverPromocaoCommand : IRequest<Result<string>>
{
    [Required(ErrorMessage = "O código da promoção é obrigatório.")]
    [SwaggerSchema("Código da promoção", Nullable = false)]
    public int PromocaoId { get; set; }
}