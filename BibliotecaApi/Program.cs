using BibliotecaApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Area de servicios

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers().AddJsonOptions(
    op => op.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer("name=DefaultConnection"));

var app = builder.Build();

// Area de middlewares

app.MapControllers();

app.Run();
