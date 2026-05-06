using System.Security.Claims;
using System.Text.Json;
using Clothub.Application.Auth.Commands.GoogleAuth;
using Clothub.Application.Auth.Commands.LoginWithEmail;
using Clothub.Application.Auth.Commands.RegisterWithEmail;
using Clothub.Application.Auth.Interfaces;
using Clothub.Application.Auth.Services;
using Clothub.API.Auth;
using Clothub.Persistence;
using Clothub.Persistence.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<ClothubDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterWithEmailCommand).Assembly));
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Google:ClientId"]!;
    options.ClientSecret = builder.Configuration["Google:ClientSecret"]!;
    options.CallbackPath = "/auth/google/callback";

    options.Events.OnTicketReceived = async ctx =>
    {
        var principal = ctx.Principal!;
        var email = principal.FindFirstValue(ClaimTypes.Email);
        if (email is null)
        {
            ctx.HandleResponse();
            ctx.Response.StatusCode = StatusCodes.Status400BadRequest;
            return;
        }

        var mediator = ctx.HttpContext.RequestServices.GetRequiredService<IMediator>();
        var googleId = principal.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var nombre = principal.FindFirstValue(ClaimTypes.GivenName) ?? string.Empty;
        var apellidos = principal.FindFirstValue(ClaimTypes.Surname) ?? string.Empty;

        var token = await mediator.Send(new GoogleAuthCommand(googleId, email, nombre, apellidos));

        ctx.HandleResponse();
        ctx.Response.ContentType = "application/json";
        await ctx.Response.WriteAsync(JsonSerializer.Serialize(new { token }));
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

if (!app.Environment.IsDevelopment())
    app.UseHttpsRedirection();

app.UseAuthentication();

app.MapPost("/auth/register", async (RegisterRequest request, IMediator mediator) =>
{
    var command = new RegisterWithEmailCommand(request.Nombre, request.Apellidos, request.Email, request.Password);
    var result = await mediator.Send(command);
    return Results.Created($"/usuarios/{result}", new { id = result });
});

app.MapPost("/auth/login", async (LoginRequest request, IMediator mediator) =>
{
    var command = new LoginWithEmailCommand(request.Email, request.Password);
    var result = await mediator.Send(command);
    return Results.Ok(new { token = result });
});

app.MapGet("/auth/google", () =>
    Results.Challenge(new AuthenticationProperties(), [GoogleDefaults.AuthenticationScheme]));

app.Run();
