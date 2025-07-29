using DominexRateOrchestrator.Application.Interfaces;
using DominexRateOrchestrator.Application.Services;
using Polly;
using Polly.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Leer configuración de servicios desde variables de entorno o appsettings
var banreservasUrl = builder.Configuration["Services:Banreservas"] ?? "https://localhost:5001";
var popularUrl = builder.Configuration["Services:Popular"] ?? "https://localhost:5003";
var bhdleonUrl = builder.Configuration["Services:BhdLeon"] ?? "https://localhost:5005";

// Mostrar configuración actual en consola (útil para Docker)
Console.WriteLine("== Configuración de servicios ==");
Console.WriteLine($"Banreservas: {banreservasUrl}");
Console.WriteLine($"Popular:     {popularUrl}");
Console.WriteLine($"BHD León:    {bhdleonUrl}");

// Polly Policies
var retryPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

var circuitBreakerPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .CircuitBreakerAsync(2, TimeSpan.FromSeconds(30));

// Configurar HttpClient para cada proveedor
builder.Services.AddHttpClient("Banreservas", client =>
{
    client.BaseAddress = new Uri(banreservasUrl);
}).AddPolicyHandler(retryPolicy)
  .AddPolicyHandler(circuitBreakerPolicy);

builder.Services.AddHttpClient("Popular", client =>
{
    client.BaseAddress = new Uri(popularUrl);
}).AddPolicyHandler(retryPolicy)
  .AddPolicyHandler(circuitBreakerPolicy);

builder.Services.AddHttpClient("BhdLeon", client =>
{
    client.BaseAddress = new Uri(bhdleonUrl);
}).AddPolicyHandler(retryPolicy)
  .AddPolicyHandler(circuitBreakerPolicy);

// Inyecciones concretas por proveedor
builder.Services.AddTransient<IExchangeRateProvider>(sp =>
    new BanreservasExchangeRateProvider(sp.GetRequiredService<IHttpClientFactory>().CreateClient("Banreservas")));

builder.Services.AddTransient<IExchangeRateProvider>(sp =>
    new BancoPopularExchangeRateProvider(sp.GetRequiredService<IHttpClientFactory>().CreateClient("Popular")));

builder.Services.AddTransient<IExchangeRateProvider>(sp =>
    new BancoBhdLeonExchangeRateProvider(sp.GetRequiredService<IHttpClientFactory>().CreateClient("BhdLeon")));

// Orquestador principal
builder.Services.AddScoped<IExchangeAggregatorService, ExchangeAggregatorService>();

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
