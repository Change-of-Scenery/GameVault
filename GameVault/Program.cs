using GameVault.Components;
using GameVault.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register API controllers
builder.Services.AddControllers();

// Register OpenAPI for API documentation
builder.Services.AddOpenApi();

// Register EF Core DbContext with SQL Server provider
builder.Services.AddDbContext<GameVaultDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Map API controllers
app.MapControllers();

// Map OpenAPI specification endpoint
app.MapOpenApi();

// Map Scalar API documentation UI
app.MapScalarApiReference();

app.Run();
