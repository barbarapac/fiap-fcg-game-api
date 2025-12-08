using Fiap.FCG.Game.Domain.Jogos;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.FCG.Game.Unit.Test.Application.Compras.Comprar.Mocks
{
    public class JogoRepositoryMock : Mock<IJogoRepository>
    {
        public void ConfigurarObterPorIdsAsync(List<Jogo> jogos)
        {
            Setup(x => x.ObterPorIdsAsync(It.IsAny<List<int>>()))
                .ReturnsAsync(jogos);
        }

        public void ConfigurarObterPorIdsAsyncParaRetornarVazio()
        {
            Setup(x => x.ObterPorIdsAsync(It.IsAny<List<int>>()))
                .ReturnsAsync(new List<Jogo>());
        }
    }
}
