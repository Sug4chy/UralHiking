using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using UralHiking.Database;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions { WebRootPath = "static" });

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContext>(options =>
    options
        .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        .UseSnakeCaseNamingConvention()
);

var app = builder.Build();

if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await using (var scope = app.Services.CreateAsyncScope())
{
    await using var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    if ((await db.Database.GetPendingMigrationsAsync()).Any())
    {
        await db.Database.MigrateAsync();
    }
}

app.UseStaticFiles();
app.MapControllers();
app.Run();