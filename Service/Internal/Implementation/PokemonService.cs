using Domain.Model.Implementation;
using FluentResults;
using Infrastructure.Repository;
using Microsoft.Extensions.Logging;

namespace Service.Internal.Implementation;

public class PokemonService : IPokemonService
{
    private readonly ILogger<PokemonService> _logger;
    private readonly IMongoRepository<Pokemon> _repository;

    public PokemonService(IMongoRepository<Pokemon> repository, ILogger<PokemonService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task InsertPokemonAsync(Pokemon param)
    {
        await _repository.InsertOneAsync(param);
    }

    public async Task<Pokemon> FindPokemonByIdAsync(string id)
    {
        return await _repository.FindByIdAsync(id);
    }
}