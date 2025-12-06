using AutoBogus;
using Fiap.FCG.Game.Application.Compras.Consultar;

namespace Fiap.FCG.Game.Unit.Test.Application.Compras.Consultar.Fakers
{
    public static class ConsultarBibliotecaQueryFaker
    {
        public static ConsultarBibliotecaQuery ComValido() =>
        new AutoFaker<ConsultarBibliotecaQuery>()
        .RuleFor(x => x.UsuarioId, f => f.Random.Long(1))
        .Generate();
    }
}
