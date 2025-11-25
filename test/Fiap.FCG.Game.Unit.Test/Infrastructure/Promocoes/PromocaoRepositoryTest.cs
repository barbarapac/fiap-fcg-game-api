using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fiap.FCG.Game.Domain.Promocoes;
using Fiap.FCG.Game.Infrastructure.Promocoes;
using Fiap.FCG.Game.Unit.Test._Shared;
using Fiap.FCG.Game.Unit.Test.Infrastructure.Promocoes.Fakers;
using Fiap.FCG.Game.Unit.Test.Infrastructure.Promocoes.Fixtures;
using FluentAssertions;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.Infrastructure.Promocoes;

public class PromocaoRepositoryTest
{
    [Fact]
    public async Task AtualizarAsync_QuandoPromocaoValida_DeveAtualizarComSucesso()
    {
        using var fixture = new PromocaoRepositoryFixture();
        var promocao = PromocaoFaker.Valida();
        await fixture.Context.Set<Promocao>().AddAsync(promocao);
        await fixture.Context.SaveChangesAsync();

        promocao.Atualizar("Nova Promo", "Atualizada", 50, DateTime.Today, DateTime.Today.AddDays(5));

        var result = await fixture.Repository.AtualizarAsync(promocao);

        result.Sucesso.Should().BeTrue();
        result.Valor.Should().Be("Promoção atualizada com sucesso");

        var atualizado = await fixture.Context.Set<Promocao>().FindAsync(promocao.Id);
        atualizado!.Nome.Should().Be("Nova Promo");
    }

    [Fact]
    public async Task AtualizarAsync_QuandoErro_DeveRetornarFalha()
    {
        var context = ContextFactory.Create();
        var repository = new PromocaoRepository(context);
        var promocao = PromocaoFaker.Valida();
        context.Dispose();

        var result = await repository.AtualizarAsync(promocao);

        result.Sucesso.Should().BeFalse();
        result.Erro.Should().Contain("Erro ao atualizar promoção");
    }

    [Fact]
    public async Task ObterTodasAsync_DeveRetornarTodasAsPromocoes()
    {
        using var fixture = new PromocaoRepositoryFixture();
        var promocao = PromocaoFaker.Valida();
        await fixture.Context.Set<Promocao>().AddAsync(promocao);
        await fixture.Context.SaveChangesAsync();

        var resultado = await fixture.Repository.ObterTodasAsync();

        resultado.Should().NotBeEmpty();
        resultado.Should().ContainSingle(x => x.Id == promocao.Id);
    }

    [Fact]
    public async Task ObterPromocoesAtivasComJogosAsync_DeveRetornarSomenteAtivas()
    {
        using var fixture = new PromocaoRepositoryFixture();
        var promocao = PromocaoFaker.Valida();
        await fixture.Context.Set<Promocao>().AddAsync(promocao);
        await fixture.Context.SaveChangesAsync();

        var resultado = await fixture.Repository.ObterPromocoesAtivasComJogosAsync();

        resultado.Should().NotBeEmpty();
        resultado.Should().Contain(x => x.Id == promocao.Id);
    }

    [Fact]
    public async Task ObterPorJogosIdsAsync_QuandoRelacionamentoExiste_DeveRetornarPromocaoJogo()
    {
        using var fixture = new PromocaoRepositoryFixture();
        var promocaoJogo = PromocaoJogoFaker.Valido();

        await fixture.Context.Set<Promocao>().AddAsync(promocaoJogo.Promocao);
        await fixture.Context.Set<Fiap.FCG.Game.Domain.Jogos.Jogo>().AddAsync(promocaoJogo.Jogo);
        await fixture.Context.Set<PromocaoJogo>().AddAsync(promocaoJogo);
        await fixture.Context.SaveChangesAsync();

        var resultado = await fixture.Repository.ObterPorJogosIdsAsync(new List<int> { promocaoJogo.JogoId });

        resultado.Should().ContainSingle(x => x.JogoId == promocaoJogo.JogoId && x.PromocaoId == promocaoJogo.PromocaoId);
    }

    [Fact]
    public async Task ObterPorIdAsync_QuandoExiste_DeveRetornarComJogos()
    {
        using var fixture = new PromocaoRepositoryFixture();
        var promocaoJogo = PromocaoJogoFaker.Valido();

        await fixture.Context.Set<Promocao>().AddAsync(promocaoJogo.Promocao);
        await fixture.Context.Set<Fiap.FCG.Game.Domain.Jogos.Jogo>().AddAsync(promocaoJogo.Jogo);
        await fixture.Context.Set<PromocaoJogo>().AddAsync(promocaoJogo);
        await fixture.Context.SaveChangesAsync();

        var resultado = await fixture.Repository.ObterPorIdAsync(promocaoJogo.Promocao.Id);

        resultado.Should().NotBeNull();
        resultado!.Jogos.Should().ContainSingle(j => j.JogoId == promocaoJogo.JogoId);
    }
}