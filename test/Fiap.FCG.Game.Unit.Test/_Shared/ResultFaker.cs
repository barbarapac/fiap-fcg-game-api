using Fiap.FCG.Game.Domain._Shared;

namespace Fiap.FCG.Game.Unit.Test._Shared;

public static class ResultFaker
{
    public static Result<string> Sucesso(string valor)
        => Result.Success(valor);

    public static Result<string> Falha(string erro)
        => Result.Failure<string>(erro);
    
    public static Result<bool> Sucesso()
        => Result.Success(true);
    
    public static Result<bool> FalhaBool(string erro)
        => Result.Failure<bool>(erro);

}