using LotoNET.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using LotoNET.Application.Interfaces;
using LotoNET.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using LotoNET.API.Conventions;
using DotNetEnv;

// Carrega variáveis do .env
Env.Load();

// Criação do builder
var builder = WebApplication.CreateBuilder(args);

// Monta a connection string manualmente com base no .env
var host = Environment.GetEnvironmentVariable("POSTGRES_HOST");
var port = Environment.GetEnvironmentVariable("POSTGRES_PORT");
var user = Environment.GetEnvironmentVariable("POSTGRES_USER");
var password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");
var database = Environment.GetEnvironmentVariable("POSTGRES_DB");

var connectionString = $"Host={host};Port={port};Username={user};Password={password};Database={database}";

// Injeção do DbContext
builder.Services.AddDbContext<LotoNetDbContext>(options =>
    options.UseNpgsql(connectionString));

// Serviços da aplicação
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IJsonImportService, JsonImportService>();
builder.Services.AddScoped<ILotteryImporter, LotteryImporter>();
builder.Services.AddScoped<ILotteryApiService, LotteryApiService>();

// HttpClient necessário para resolver IHttpClientFactory em AuthService e LotteryApiService
builder.Services.AddHttpClient();

// Controllers com rotas slugificadas
builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Build da aplicação
var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

// Importação inicial de dados das loterias
using (var scope = app.Services.CreateScope())
{
    var importer = scope.ServiceProvider.GetRequiredService<ILotteryImporter>();

    var loterias = new[] { "megasena", "quina", "lotofacil" };

    foreach (var nome in loterias)
    {
        await importer.ImportFromApiAsync(nome);
    }

    // Importação local comentada
    /*
    var importerLocal = scope.ServiceProvider.GetRequiredService<IJsonImportService>();
    var dataDir = Path.Combine(Directory.GetCurrentDirectory(), "data");

    var arquivos = new[]
    {
        Path.Combine(dataDir, "megasena.json"),
        Path.Combine(dataDir, "lotofacil.json"),
        Path.Combine(dataDir, "quina.json")
    };

    foreach (var arquivo in arquivos)
    {
        if (File.Exists(arquivo))
        {
            Console.WriteLine($"\n--- Importando: {Path.GetFileName(arquivo)} ---");
            await importerLocal.ImportFromFileAsync(arquivo);
        }
        else
        {
            Console.WriteLine($"❌ Arquivo não encontrado: {arquivo}");
        }
    }
    */
}

app.Run();
