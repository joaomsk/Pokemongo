using System.Net.Http.Json;

namespace Service.External.Implementation
{
    public class PokeApiService : IPokeApiService
    {
        private readonly HttpClient _httpClient;

        public PokeApiService(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<object?> GetExternalPokemon(string name)
        {
            object? result =  await _httpClient
                .GetFromJsonAsync<object>($"{name}");

            return result;
        }
    }
}
