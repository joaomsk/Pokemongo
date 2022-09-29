using Domain.Model.Implementation;
using FluentResults;

namespace Service.Internal;

public interface IPokemonService
{
    Task InsertPokemonAsync(Pokemon param);
    Task<Pokemon> FindPokemonByIdAsync(string id);
}