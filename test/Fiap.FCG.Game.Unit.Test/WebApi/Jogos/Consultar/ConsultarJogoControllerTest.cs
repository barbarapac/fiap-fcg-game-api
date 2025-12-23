using Fiap.FCG.Game.Domain.Jogos;
using Fiap.FCG.Game.Unit.Test.WebApi.Jogos.Consultar.Fakers;
using Fiap.FCG.Game.Unit.Test.WebApi.Jogos.Consultar.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Fiap.FCG.Game.Unit.Test.WebApi.Jogos.Consultar;

public class ConsultarJogoControllerTest : ConsultarJogoControllerFixture
{
    [Fact]
    public async Task ObterPorId_QuandoJogoExiste_DeveRetornarOk()
    {
        // Arrange
        var jogo = JogoResponseFaker.Valido();
        ConsultaMock.ConfigurarObterPorId(jogo.Id, jogo);

        // Act
        var resultado = await Controller.ObterPorId(jogo.Id);

        // Assert
        var ok = resultado.Should().BeOfType<OkObjectResult>().Subject;
        ok.Value.Should().BeEquivalentTo(new { sucesso = true, jogo });

        ConsultaMock.GarantirObterPorIdChamado();
    }

    [Fact]
    public async Task ObterPorId_QuandoJogoNaoExiste_DeveRetornarNotFound()
    {
        // Arrange
        const int id = 999;
        ConsultaMock.ConfigurarObterPorId(id, null);

        // Act
        var resultado = await Controller.ObterPorId(id);

        // Assert
        var notFound = resultado.Should().BeOfType<NotFoundObjectResult>().Subject;
        notFound.Value.Should().BeEquivalentTo(new 
        { 
            sucesso = false, 
            mensagem = "Jogo não encontrado." 
        });

        ConsultaMock.GarantirObterPorIdChamado();
    }

    [Fact]
    public async Task ObterTodos_QuandoJogosExistem_DeveRetornarOk()
    {
        // Arrange
        var jogos = JogoResponseFaker.Lista();
        ConsultaMock.ConfigurarObterTodos(jogos);

        // Act
        var resultado = await Controller.ObterTodos();

        // Assert
        var ok = resultado.Should().BeOfType<OkObjectResult>().Subject;
        ok.Value.Should().BeEquivalentTo(new 
        { 
            sucesso = true, 
            jogos 
        });

        ConsultaMock.GarantirObterTodosChamado();
    }

    [Fact]
    public async Task ObterPorNomeOrdenado_DeveRetornarOk()
    {
        // Arrange
        var jogos = JogoResponseFaker.Lista();
        string nome = "Fifa";
        string tipo = "Esporte";
        bool crescente = true;
        var jogosJson = JsonSerializer.Serialize(jogos);
        ConsultaMock.ConfigurarObterPorNomeOrdenado(nome, tipo, crescente, jogosJson);

        // Act
        var resultado = await Controller.ObterPorNomeOrdenado(nome, tipo, crescente);

        // Assert
        var ok = resultado.Should().BeOfType<OkObjectResult>().Subject;
        var jogosElement = (JsonElement)ok.Value.GetType().GetProperty("jogos")!.GetValue(ok.Value)!;
        var jsonRetornado = jogosElement.GetRawText();
        jsonRetornado.Should().BeEquivalentTo(jogosJson);
        ConsultaMock.GarantirObterPorNomeOrdenadoChamado();
    }

    [Fact]
    public async Task ObterMetricasPreco_DeveRetornarOk()
    {
        // Arrange
        var metricas = new { Min = 10.0, Max = 100.0, Media = 55.0 };
        var metricasJson = JsonSerializer.Serialize(metricas);
        ConsultaMock.ConfigurarObterMetricasPreco(metricasJson);

        // Act
        var resultado = await Controller.ObterMetricasPreco();

        // Assert
        var ok = resultado.Should().BeOfType<OkObjectResult>().Subject;
        var jogosElement = (JsonElement)ok.Value.GetType().GetProperty("jogos")!.GetValue(ok.Value)!;
        var jsonRetornado = jogosElement.GetRawText();  
        jsonRetornado.Should().BeEquivalentTo(metricasJson);
        ConsultaMock.GarantirObterMetricasPrecoChamado();
    }
                
    [Fact]
    public async Task ObterContagemPorTipo_DeveRetornarOk()
    {
        // Arrange
        var contagem = new[] { new { Tipo = "Aventura", Quantidade = 5 }, new { Tipo = "Esporte", Quantidade = 3 } };
        var contagemJson = JsonSerializer.Serialize(contagem);
        ConsultaMock.ConfigurarObterContagemPorTipo(contagemJson);

        // Act
        var resultado = await Controller.ObterContagemPorTipo();

        // Assert
        var ok = resultado.Should().BeOfType<OkObjectResult>().Subject;
        var jogosElement = (JsonElement)ok.Value.GetType().GetProperty("jogos")!.GetValue(ok.Value)!;
        var jsonRetornado = jogosElement.GetRawText();
        jsonRetornado.Should().BeEquivalentTo(contagemJson);
        ConsultaMock.GarantirObterContagemPorTipoChamado();
    }

    [Fact]
    public async Task ObterJogosMaisCarosOuBaratos_DeveRetornarOk()
    {
        // Arrange
        bool maisCaros = true;
        int quantidade = 2;
        var jogos = JogoResponseFaker.Lista();
        var jogosJson = JsonSerializer.Serialize(jogos);
        ConsultaMock.ConfigurarObterJogosMaisCarosOuBaratos(maisCaros, quantidade, jogosJson);

        // Act
        var resultado = await Controller.ObterJogosMaisCarosOuBaratos(maisCaros, quantidade);

        // Assert
        var ok = resultado.Should().BeOfType<OkObjectResult>().Subject;
        var jogosElement = (JsonElement)ok.Value.GetType().GetProperty("jogos")!.GetValue(ok.Value)!;
        var jsonRetornado = jogosElement.GetRawText();
        jsonRetornado.Should().BeEquivalentTo(jogosJson);
        ConsultaMock.GarantirObterJogosMaisCarosOuBaratosChamado();
    }

    [Fact]
    public async Task ObterPorTipoEPreco_DeveRetornarOk()
    {
        // Arrange
        string tipo = "Aventura";
        double precoMin = 10.0;
        double precoMax = 50.0;
        var jogos = JogoResponseFaker.Lista();
        var jogosJson = JsonSerializer.Serialize(jogos);
        ConsultaMock.ConfigurarObterPorTipoEPreco(tipo, precoMin, precoMax, jogosJson);

        // Act
        var resultado = await Controller.ObterPorTipoEPreco(tipo, precoMin, precoMax);

        // Assert
        var ok = resultado.Should().BeOfType<OkObjectResult>().Subject;
        var jogosElement = (JsonElement)ok.Value.GetType().GetProperty("jogos")!.GetValue(ok.Value)!;
        var jsonRetornado = jogosElement.GetRawText();
        jsonRetornado.Should().BeEquivalentTo(jogosJson);
        ConsultaMock.GarantirObterPorTipoEPrecoChamado();
    }
}