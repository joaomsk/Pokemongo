using Domain.Model.Implementation;
using Microsoft.AspNetCore.Mvc;
using Service.Internal;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class PokemonController : ControllerBase
{
    private readonly IPokemonService _service;

    public PokemonController(IPokemonService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> InsertPokemon([FromBody] Pokemon param)
    {
        await _service.InsertPokemonAsync(param);
        return Created("Pokemon created successfully", param);
    }

    [HttpGet("/{id}")]
    public async Task<IActionResult> GetPokemon([FromRoute] string id)
    {
        return Ok(await _service.FindPokemonByIdAsync(id));
    }
}