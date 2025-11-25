using System;
using System.Collections.Generic;
using System.Linq;
using AutoBogus;
using Fiap.FCG.Game.Domain.Jogos;
using Fiap.FCG.Game.Domain.Promocoes;

namespace Fiap.FCG.Game.Unit.Test.Application.Promocoes.Consultar.Fakers;

public static class PromocaoFaker
{
    public static List<Jogo> JogosValidos(List<int> ids)
    {
        return ids.Select(id => new AutoFaker<Jogo>()
            .RuleFor(j => j.Id, id)
            .Generate()).ToList();
    }

    public static Promocao Valida()
    {
        var result = Promocao.Criar(
            "Black Friday",
            "Desconto especial",
            20,
            DateTime.Today,
            DateTime.Today.AddDays(7)
        );

        return result.Valor;
    }
    
    public static Promocao Gerar()
    {
        var promocao = Promocao.Criar(
            "Promo Teste",
            "Gerada via faker",
            30,
            DateTime.Today,
            DateTime.Today.AddDays(10)
        ).Valor;
        
        promocao.AdicionarJogos(new []{1,2});

        foreach (var promocaoJogo in promocao.Jogos)
        {
            promocaoJogo.Jogo = new Jogo();
        }

        return promocao;
    }
}