using System.Threading.Tasks;

namespace Fiap.FCG.Game.Application.Jogos.Consultar;

public interface IElasticConnector
{
    Task<string> SearchAsync(string index, string queryJson);
}
