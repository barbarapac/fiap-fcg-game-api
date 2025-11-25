using System.Collections.Generic;
using AutoBogus;
using Fiap.FCG.Game.Domain.Jogos;
using Fiap.FCG.Game.Domain.Promocoes;

namespace Fiap.FCG.Game.Unit.Test.Application.Notificacoes.Consultar.Fakers;

public static class PromocaoFaker
{
    public static List<Promocao> ComJogos(int quantidade)
    {
        var lista = new List<Promocao>();

        for (var i = 0; i < quantidade; i++)
        {
            var promocao = new Promocao();

            var promocaoJogos = new AutoFaker<PromocaoJogo>().Generate(2);

            foreach (var promocaoJogo in promocaoJogos)
            {
                var jogo = new AutoFaker<Jogo>().Generate();
                promocaoJogo.AdicionarJogo(jogo);
                promocao.Jogos.Add(promocaoJogo);
            }

            lista.Add(promocao);
        }

        return lista;
    }
    
    public static List<Promocao> ComUmJogo()
    {
        var lista = new List<Promocao>(); 
        var promocaoJogos = new AutoFaker<PromocaoJogo>().Generate(1);

        var promocao = new Promocao();
        foreach (var promocaoJogo in promocaoJogos)
        {
            var jogo = new AutoFaker<Jogo>().Generate();
            promocaoJogo.AdicionarJogo(jogo);
            promocao.Jogos.Add(promocaoJogo);
        }

        lista.Add(promocao);

        return lista;
    }
}

