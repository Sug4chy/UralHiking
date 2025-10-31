using Microsoft.EntityFrameworkCore;
using UralHiking.Database;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions { WebRootPath = "static" });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
await using var db = app.Services.GetRequiredService<DatabaseContext>();
await db.Database.MigrateAsync();

app.UseStaticFiles();
app.MapControllers();
app.Run();