using BibliotecaApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Area de servicios
builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer("name=DefaultConnection"));

var app = builder.Build();

// Area de middlewares

app.MapControllers();

app.Run();
