using AdvancedSearch.Application.Features.Products.Command.CreateProduct;
using AdvancedSearch.Application.Interfaces;
using AdvancedSearch.Domain.Interfaces.Services;
using AdvancedSearch.Infrastructure.BackgroundServices;
using AdvancedSearch.Infrastructure.Context;
using AdvancedSearch.Infrastructure.Services;
using AdvancedSearch.Infrastructure.UnitOfWork;
using AdvancedSearchDomain.Interfaces.Services;
using AdvancedSearchDomain.Interfaces.UnitOfWork;
using AdvancedSearchDomain.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), o => o.UseVector()));

builder.Services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICurrentUserService, FakeCurrentUserService>();

builder.Services.AddHttpClient<IEmbeddingService, OllamaEmbeddingService>();

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
