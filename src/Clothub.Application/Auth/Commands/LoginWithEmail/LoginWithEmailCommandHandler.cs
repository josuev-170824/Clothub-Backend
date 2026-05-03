using Clothub.Application.Auth.Interfaces;
using MediatR;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace Clothub.Application.Auth.Commands.LoginWithEmail;

public class LoginWithEmailCommandHandler : IRequestHandler<LoginWithEmailCommand, string>
{
    private readonly IConfiguration _configuration;

    private readonly IUsuarioRepository _usuarioRepository;

    public LoginWithEmailCommandHandler(IUsuarioRepository usuarioRepository, IConfiguration configuration)
    {
        _usuarioRepository = usuarioRepository;
        _configuration = configuration;
    }

    public async Task<string> Handle(LoginWithEmailCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.ObtenerPorEmailAsync(request.Email, cancellationToken);                                                                  
                       
        if (usuario == null)
            throw new InvalidOperationException("Credenciales inválidas.");

        if (!BCrypt.Net.BCrypt.Verify(request.Password, usuario.PasswordHash))
            throw new InvalidOperationException("Credenciales inválidas.");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.Role, usuario.Rol.ToString()),
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: credentials
        );

      return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
