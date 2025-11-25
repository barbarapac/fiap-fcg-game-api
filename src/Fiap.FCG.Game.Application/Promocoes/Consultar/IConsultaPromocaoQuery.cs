using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fiap.FCG.Game.Application.Promocoes.Consultar;

public interface IConsultaPromocaoQuery
{
    Task<PromocaoResponse> ObterPorIdAsync(int id);
    Task<List<PromocaoResponse>> ObterTodasAsync();
}