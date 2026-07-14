using AdvancedSearch.Application.Features.Products.Command.CreateProduct;
using AdvancedSearch.Application.Interfaces;
using AdvancedSearch.Infrastructure.BackgroundServices;
using AdvancedSearch.Infrastructure.Context;
using AdvancedSearch.Infrastructure.Options;
using AdvancedSearch.Infrastructure.Services;
using AdvancedSearch.Infrastructure.UnitOfWork;
using AdvancedSearchDomain.Interfaces.Services;
using AdvancedSearchDomain.Interfaces.UnitOfWork;
using AdvancedSearchDomain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using AdvancedSearch.Application.Interfaces.AI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), o => o.UseVector()));

builder.Services.AddOptions<OllamaOptions>()
    .Bind(builder.Configuration.GetSection(OllamaOptions.SectionName))
    .Validate(options => !string.IsNullOrWhiteSpace(options.BaseUrl), "Ollama BaseUrl is required.")
    .Validate(options => Uri.TryCreate(options.BaseUrl, UriKind.Absolute, out _), "Ollama BaseUrl must be a valid absolute URL.")
    .Validate(options => !string.IsNullOrWhiteSpace(options.EmbeddingModel), "Ollama EmbeddingModel is required.")
    .Validate(options => !string.IsNullOrWhiteSpace(options.ChatModel), "Ollama ChatModel is required.")
    .Validate(options => options.RequestTimeoutSeconds > 0, "Ollama timeout must be greater than zero.")
    .ValidateOnStart();//Uygulama ayaða kalkarken hemen patlasýn, hatayý erken yakalamak için ValidateOnStart() kullanýyoruz.

builder.Services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICurrentUserService, FakeCurrentUserService>();

builder.Services.AddHttpClient<IEmbeddingService, OllamaEmbeddingService>((serviceProvider, httpClient) =>
{
    var options = serviceProvider.GetRequiredService<IOptions<OllamaOptions>>().Value;

    httpClient.BaseAddress = new Uri(options.BaseUrl);
    httpClient.Timeout = TimeSpan.FromSeconds(options.RequestTimeoutSeconds);
});

builder.Services.AddHttpClient<IRagService, OllamaRagService>((serviceProvider, httpClient) =>
{
    var options = serviceProvider.GetRequiredService<IOptions<OllamaOptions>>().Value;

    httpClient.BaseAddress = new Uri(options.BaseUrl);
    httpClient.Timeout = TimeSpan.FromSeconds(options.RequestTimeoutSeconds);
});

builder.Services.AddScoped<ICommentPolicyService, CommentPolicyService>();

builder.Services.AddHostedService<CategoriesEmbeddingMigrationService>();
builder.Services.AddHostedService<ProductsEmbeddingMigrationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
