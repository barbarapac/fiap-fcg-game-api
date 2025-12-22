using System.Collections.Generic;
using Fiap.FCG.Game.Application.Jogos.Consultar;
using Moq;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Jogos.Consultar.Mocks;

public class ConsultaJogoQueryMock : Mock<IConsultaJogoQuery>
{
    public void ConfigurarObterPorId(int id, JogoResponse response)
    {
        Setup(c => c.ObterPorIdAsync(id))
            .ReturnsAsync(response);
    }

    public void ConfigurarObterTodos(List<JogoResponse> jogos)
    {
        Setup(c => c.ObterTodosAsync())
            .ReturnsAsync(jogos);
    }

    public void GarantirObterPorIdChamado()
    {
        Verify(c => c.ObterPorIdAsync(It.IsAny<int>()), Times.Once);
    }

    public void GarantirObterTodosChamado()
    {
        Verify(c => c.ObterTodosAsync(), Times.Once);
    }

    public void ConfigurarObterPorNomeOrdenado(string nome, string tipo, bool crescente, string jogos)
    {
        Setup(c => c.ObterPorNomeOrdenadoAsync(nome, tipo, crescente))
            .ReturnsAsync(jogos);
    }

    public void GarantirObterPorNomeOrdenadoChamado()
    {
        Verify(c => c.ObterPorNomeOrdenadoAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Once);
    }

    public void ConfigurarObterMetricasPreco(string metricas)
    {
        Setup(c => c.ObterMetricasPrecoAsync())
            .ReturnsAsync(metricas);
    }

    public void GarantirObterMetricasPrecoChamado()
    {
        Verify(c => c.ObterMetricasPrecoAsync(), Times.Once);
    }

    public void ConfigurarObterContagemPorTipo(string contagem)
    {
        Setup(c => c.ObterContagemPorTipoAsync())
            .ReturnsAsync(contagem);
    }

    public void GarantirObterContagemPorTipoChamado()
    {
        Verify(c => c.ObterContagemPorTipoAsync(), Times.Once);
    }

    public void ConfigurarObterJogosMaisCarosOuBaratos(bool maisCaros, int quantidade, string jogos)
    {
        Setup(c => c.ObterJogosMaisCarosOuBaratosAsync(maisCaros, quantidade))
            .ReturnsAsync(jogos);
    }

    public void GarantirObterJogosMaisCarosOuBaratosChamado()
    {
        Verify(c => c.ObterJogosMaisCarosOuBaratosAsync(It.IsAny<bool>(), It.IsAny<int>()), Times.Once);
    }

    public void ConfigurarObterPorTipoEPreco(string tipo, double precoMin, double precoMax, string jogos)
    {
        Setup(c => c.ObterPorTipoEPrecoAsync(tipo, precoMin, precoMax))
            .ReturnsAsync(jogos);
    }

    public void GarantirObterPorTipoEPrecoChamado()
    {
        Verify(c => c.ObterPorTipoEPrecoAsync(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<double>()), Times.Once);
    }
}