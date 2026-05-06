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
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        var origins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
            ?? ["http://localhost:5173"];
        policy.WithOrigins(origins)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

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
        var frontendUrl = ctx.HttpContext.RequestServices.GetRequiredService<IConfiguration>()["Frontend:Url"]!;

        ctx.HandleResponse();
        ctx.Response.Redirect($"{frontendUrl}/auth/callback?token={token}");
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

if (!app.Environment.IsDevelopment())
    app.UseHttpsRedirection();

app.UseCors();
app.UseAuthentication();

app.MapPost("/auth/register", async (RegisterRequest request, IMediator mediator) =>
{
    try
    {
        var command = new RegisterWithEmailCommand(request.Nombre, request.Apellidos, request.Email, request.Password);
        var result = await mediator.Send(command);
        return Results.Created($"/usuarios/{result}", new { id = result });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { message = ex.Message });
    }
});

app.MapPost("/auth/login", async (LoginRequest request, IMediator mediator) =>
{
    try
    {
        var command = new LoginWithEmailCommand(request.Email, request.Password);
        var result = await mediator.Send(command);
        return Results.Ok(new { token = result });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { message = ex.Message });
    }
});

app.MapGet("/auth/google", () =>
    Results.Challenge(new AuthenticationProperties(), [GoogleDefaults.AuthenticationScheme]));

app.Run();
