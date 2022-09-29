using Infrastructure.Repository;
using Infrastructure.Repository.implementation;
using Infrastructure.Settings;
using Infrastructure.Settings.Implementation;
using Microsoft.Extensions.Options;
using Service.External;
using Service.External.Implementation;
using Service.Internal;
using Service.Internal.Implementation;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region CONTROLLER SETTINGS

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

#endregion

#region INFRASTRUCTURE SETTINGS

builder.Services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
builder.Services.AddSingleton<IMongoDbSettings>(serviceProvider
    => serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

#endregion

#region SERVICE SETTINGS

builder.Services.AddScoped<IPokemonService, PokemonService>();
builder.Services.AddScoped<IPokeApiService, PokeApiService>();
builder.Services.AddSingleton<IPokeApiSettings>(serviceProvider =>
    serviceProvider.GetRequiredService<IOptions<PokeApiSettings>>().Value);
builder.Services.Configure<PokeApiSettings>(builder.Configuration.GetSection("PokeApiService"));

builder.Services.AddHttpClient<IPokeApiService, PokeApiService>(client =>
{
    client.BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon/");
});

#endregion

builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();