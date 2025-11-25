using System.Collections.Generic;
using System.Linq;
using Fiap.FCG.Game.Domain.Jogos;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.Jogos.Fakers;

public static class JogoFaker
{
    public static Jogo Valido(string nome)
    {
        var result = Jogo.Criar(nome, 199.90m);
        return result.Valor!;
    }

    public static Jogo ComNome(string nome)
    {
        var result = Jogo.Criar(nome, 150.00m);
        return result.Valor!;
    }
    
    public static List<Jogo> ListaValida(int quantidade = 3)
    {
        return Enumerable.Range(1, quantidade).Select(i => ComNome($"Jogo {i}")).ToList();
    }
}

