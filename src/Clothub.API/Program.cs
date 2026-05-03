using Clothub.Persistence;
using Microsoft.EntityFrameworkCore;
using Clothub.Application.Auth.Commands.RegisterWithEmail;
using Clothub.Application.Auth.Interfaces;
using Clothub.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<ClothubDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterWithEmailCommand).Assembly));
  builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
