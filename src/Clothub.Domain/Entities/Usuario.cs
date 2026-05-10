using System.Security.Cryptography;
using Clothub.Domain.Enums;

namespace Clothub.Domain.Entities;

public class Usuario
{
    private Usuario() { }

    public Guid Id { get; private set; }
    public string Nombre { get; private set; } = string.Empty;
    public string Apellidos { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string? PasswordHash { get; private set; }
    public bool EmailVerificado { get; private set; }
    public string? TokenVerificacionEmail { get; private set; }
    public DateTime? FechaExpiracionTokenVerificacion { get; private set; }
    public int IntentosFallidosVerificacion { get; private set; }
    public string? TokenRecuperacionPassword { get; private set; }
    public DateTime? FechaExpiracionTokenRecuperacion { get; private set; }
    public ProveedorAuth ProveedorAuth { get; private set; }
    public string? GoogleId { get; private set; }
    public Rol Rol { get; private set; }
    public DateTime FechaRegistro { get; private set; }

    public Tienda? Tienda { get; private set; }
    public ICollection<Pedido> Pedidos { get; private set; } = [];
    public ICollection<Apartado> Apartados { get; private set; } = [];
    public ICollection<Resena> Resenas { get; private set; } = [];
    public ICollection<Seguimiento> Seguimientos { get; private set; } = [];
    public ICollection<Reporte> Reportes { get; private set; } = [];

    public static Usuario CrearConEmail(string nombre, string apellidos, string email, string passwordHash)
    {
        return new Usuario
        {
            Id = Guid.NewGuid(),
            Nombre = nombre,
            Apellidos = apellidos,
            Email = email.ToLowerInvariant(),
            PasswordHash = passwordHash,
            EmailVerificado = false,
            TokenVerificacionEmail = GenerarCodigo(),
            FechaExpiracionTokenVerificacion = DateTime.UtcNow.AddMinutes(15),
            ProveedorAuth = ProveedorAuth.Local,
            Rol = Rol.Comprador,
            FechaRegistro = DateTime.UtcNow
        };
    }

    public static Usuario CrearConGoogle(string nombre, string apellidos, string email, string googleId)
    {
        return new Usuario
        {
            Id = Guid.NewGuid(),
            Nombre = nombre,
            Apellidos = apellidos,
            Email = email.ToLowerInvariant(),
            EmailVerificado = true,
            ProveedorAuth = ProveedorAuth.Google,
            GoogleId = googleId,
            Rol = Rol.Comprador,
            FechaRegistro = DateTime.UtcNow
        };
    }

    public void VerificarEmail()
    {
        EmailVerificado = true;
        TokenVerificacionEmail = null;
        FechaExpiracionTokenVerificacion = null;
        IntentosFallidosVerificacion = 0;
    }

    public void RegenerarCodigoVerificacion()
    {
        TokenVerificacionEmail = GenerarCodigo();
        FechaExpiracionTokenVerificacion = DateTime.UtcNow.AddMinutes(15);
        IntentosFallidosVerificacion = 0;
    }

    public void IncrementarIntentosFallidos() => IntentosFallidosVerificacion++;

    public void GenerarTokenRecuperacion()
    {
        TokenRecuperacionPassword = Convert.ToHexString(RandomNumberGenerator.GetBytes(32));
        FechaExpiracionTokenRecuperacion = DateTime.UtcNow.AddMinutes(15);
    }

    public void RestablecerPassword(string nuevoHash)
    {
        PasswordHash = nuevoHash;
        TokenRecuperacionPassword = null;
        FechaExpiracionTokenRecuperacion = null;
    }

    public void AsignarRolVendedor() => Rol = Rol.Vendedor;

    public void ActualizarPasswordHash(string nuevoHash) => PasswordHash = nuevoHash;

    private static string GenerarCodigo() => Random.Shared.Next(100000, 999999).ToString();
}
