using AutoBogus;
using Fiap.FCG.Game.Application.Jogos.Cadastrar;

namespace Fiap.FCG.Game.Unit.Test.Application.Jogos.Cadastrar.Fakers
{
    public class CadastrarJogoCommandFaker
    {
        public static CadastrarJogoCommand Valido()
        {
            return new AutoFaker<CadastrarJogoCommand>()
                .RuleFor(x => x.Nome, f => f.Person.FullName)
                .RuleFor(x => x.Preco, f => f.Finance.Random.Decimal())
                .Generate();
        }

        public static CadastrarJogoCommand ComNomeInvalido()
        {
            var command = Valido();
            command.Nome = "";
            return command;
        }        
    }
}
