using System.Collections.Generic;
using Fiap.FCG.Game.Domain._Shared;
using Fiap.FCG.Game.Domain.Jogos;
using Fiap.FCG.Game.Domain.Promocoes;
using Moq;

namespace Fiap.FCG.Game.Unit.Test.Application.Promocoes.Cadastrar.Mocks;

public class PromocaoRepositoryMock : Mock<IPromocaoRepository>
{
    public void ConfigurarExisteAsync(string nome, bool existe)
    {
        Setup(p => p.ExisteAsync(nome)).ReturnsAsync(existe);
    }
    
    public void JogoNaoPossuiPromocaoCadastrada()
    {
        Setup(p => p.ObterPorJogosIdsAsync(It.IsAny<List<int>>())).ReturnsAsync([]);
    }
    
    public void JogoPossuiPromocaoCadastrada()
    {
        Setup(p => p.ObterPorJogosIdsAsync(It.IsAny<List<int>>()))
            .ReturnsAsync([new PromocaoJogo { Jogo = new Jogo()}]);
    }
    
    public void GarantirAdicao()
    {
        Verify(p => p.AdicionarAsync(It.IsAny<Promocao>()), Times.Once);
    }
    
    public void ConfigurarObterPorId(Promocao? resultado)
    {
        Setup(x => x.ObterPorIdAsync(It.IsAny<int>())).ReturnsAsync(resultado);
    }

    public void ConfigurarExcluir(Promocao promocao, Result<string> resultado)
    {
        Setup(x => x.ExcluirAsync(promocao)).ReturnsAsync(resultado);
    }

    public void GarantirConsultaPorId(int id)
    {
        Verify(x => x.ObterPorIdAsync(id), Times.Once);
    }

    public void GarantirExclusao(Promocao promocao)
    {
        Verify(x => x.ExcluirAsync(promocao), Times.Once);
    }
    
    public void ConfigurarObterPorJogosIds(List<PromocaoJogo> resultado)
    {
        Setup(x => x.ObterPorJogosIdsAsync(It.IsAny<List<int>>())).ReturnsAsync(resultado);
    }

    public void ConfigurarAtualizarAsync(Result<string> resultado)
    {
        Setup(x => x.AtualizarAsync(It.IsAny<Promocao>())).ReturnsAsync(resultado);
    }
    
    public void ConfigurarObterPorIdAsync(Promocao? resultado)
    {
        Setup(x => x.ObterPorIdAsync(It.IsAny<int>())).ReturnsAsync(resultado);
    }

    public void ConfigurarObterTodasAsync(List<Promocao> resultado)
    {
        Setup(x => x.ObterTodasAsync()).ReturnsAsync(resultado);
    }
    public void ConfigurarRetornarNenhuma()
    {
        Setup(p => p.ObterPorJogosIdsAsync(It.IsAny<List<int>>()))
            .ReturnsAsync(new List<PromocaoJogo>());
    }

}