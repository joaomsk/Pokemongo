namespace Service.External
{
    public interface IPokeApiService
    {
        Task<object?> GetExternalPokemon(string name);
    }
}
