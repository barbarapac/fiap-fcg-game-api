using AutoBogus;
using Fiap.FCG.Game.Application.Jogos.Cadastrar;
using Fiap.FCG.Game.Domain.Jogos;

namespace Fiap.FCG.Game.Unit.Test.Application.Jogos.Cadastrar.Fakers
{
    public static class JogoFaker
    {
        public static Jogo ConverterParaJogo(CadastrarJogoCommand command)
        {
            return new AutoFaker<Jogo>()
                .RuleFor(x => x.Nome, f => command.Nome)
                .RuleFor(x => x.Preco, f => command.Preco)
                .Generate();
        }
    }
}
