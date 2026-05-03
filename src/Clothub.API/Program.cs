using Clothub.Persistence;
using Microsoft.EntityFrameworkCore;
using Clothub.Application.Auth.Commands.RegisterWithEmail;
using Clothub.Application.Auth.Interfaces;
using Clothub.Persistence.Repositories;
using MediatR;  
using Clothub.API.Auth;
using Clothub.Application.Auth.Commands.LoginWithEmail;


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

app.MapPost("/auth/register", async (RegisterRequest request, IMediator mediator) =>
{
    var command = new RegisterWithEmailCommand(request.Nombre, request.Apellidos, request.Email, request.Password);
    var result = await mediator.Send(command);
    return Results.Created($"/usuarios/{result}", new {id=result});
});

app.MapPost("/auth/login", async (LoginRequest request, IMediator mediator) =>
{
    var command = new LoginWithEmailCommand(request.Email, request.Password);
    var result = await mediator.Send(command);
    return Results.Ok(new {token = result});
});

app.Run();
