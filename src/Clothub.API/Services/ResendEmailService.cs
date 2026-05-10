using Clothub.Application.Auth.Services;
using Resend;

namespace Clothub.API.Services;

public class ResendEmailService : IEmailService
{
    private readonly IResend _resend;

    public ResendEmailService(IResend resend)
    {
        _resend = resend;
    }

    public async Task EnviarCodigoVerificacionAsync(string email, string nombre, string codigo, CancellationToken cancellationToken)
    {
        var message = new EmailMessage
        {
            From = "Clothub <noreply@clothubcr.com>",
            Subject = $"Tu código de verificación: {codigo}",
            HtmlBody = BuildHtml(nombre, codigo),
        };
        message.To.Add(email);

        await _resend.EmailSendAsync(message);
    }

    public async Task EnviarEmailRecuperacionPasswordAsync(string email, string nombre, string link, CancellationToken cancellationToken)
    {
        var message = new EmailMessage
        {
            From = "Clothub <noreply@clothubcr.com>",
            Subject = "Recuperá tu contraseña",
            HtmlBody = BuildRecuperacionHtml(nombre, link),
        };
        message.To.Add(email);

        await _resend.EmailSendAsync(message);
    }

    private static string BuildRecuperacionHtml(string nombre, string link) => $"""
        <!DOCTYPE html>
        <html lang="es">
        <body style="margin:0;padding:0;background:#f5f5f0;font-family:'Courier New',monospace;">
          <div style="max-width:520px;margin:40px auto;background:#fffef8;border:3px solid #1a1a1a;box-shadow:8px 8px 0 #1a1a1a;">
            <div style="background:#1a1a1a;padding:20px 32px;">
              <span style="font-size:28px;font-weight:700;letter-spacing:6px;color:#fffef8;">CLOTHUB</span>
            </div>
            <div style="padding:40px 32px;">
              <p style="font-size:13px;letter-spacing:2px;text-transform:uppercase;color:#555;margin:0 0 8px;">
                HOLA, {nombre.ToUpperInvariant()}
              </p>
              <p style="font-size:15px;color:#1a1a1a;margin:0 0 32px;line-height:1.5;">
                Recibimos una solicitud para restablecer la contraseña de tu cuenta.<br/>
                Hacé clic en el botón para crear una nueva:
              </p>
              <div style="text-align:center;margin-bottom:32px;">
                <a href="{link}" style="display:inline-block;background:#1a1a1a;color:#fffef8;padding:16px 40px;font-family:'Courier New',monospace;font-size:13px;font-weight:700;letter-spacing:3px;text-decoration:none;border:3px solid #1a1a1a;">
                  RESTABLECER CONTRASEÑA →
                </a>
              </div>
              <p style="font-size:12px;letter-spacing:1px;text-transform:uppercase;color:#888;margin:0;line-height:1.6;">
                Este link expira en 15 minutos y es de un solo uso.<br/>
                Si no solicitaste esto, ignorá este correo.
              </p>
            </div>
            <div style="background:#1a1a1a;padding:12px 32px;text-align:center;">
              <span style="font-size:11px;letter-spacing:2px;color:#888;">© 2026 CLOTHUB CR · SAN CARLOS · CR</span>
            </div>
          </div>
        </body>
        </html>
        """;

    private static string BuildHtml(string nombre, string codigo) => $"""
        <!DOCTYPE html>
        <html lang="es">
        <body style="margin:0;padding:0;background:#f5f5f0;font-family:'Courier New',monospace;">
          <div style="max-width:520px;margin:40px auto;background:#fffef8;border:3px solid #1a1a1a;box-shadow:8px 8px 0 #1a1a1a;">
            <div style="background:#1a1a1a;padding:20px 32px;">
              <span style="font-size:28px;font-weight:700;letter-spacing:6px;color:#fffef8;">CLOTHUB</span>
            </div>
            <div style="padding:40px 32px;">
              <p style="font-size:13px;letter-spacing:2px;text-transform:uppercase;color:#555;margin:0 0 8px;">
                HOLA, {nombre.ToUpperInvariant()}
              </p>
              <p style="font-size:15px;color:#1a1a1a;margin:0 0 32px;line-height:1.5;">
                Para activar tu cuenta en Clothub ingresá este código en la pantalla de verificación:
              </p>
              <div style="text-align:center;margin-bottom:32px;">
                <div style="display:inline-block;background:#f5f5f0;border:3px solid #1a1a1a;padding:20px 40px;">
                  <span style="font-size:48px;font-weight:700;letter-spacing:18px;color:#1a1a1a;">{codigo}</span>
                </div>
              </div>
              <p style="font-size:12px;letter-spacing:1px;text-transform:uppercase;color:#888;margin:0;line-height:1.6;">
                Este código expira en 15 minutos.<br/>
                Si no creaste esta cuenta, ignorá este correo.
              </p>
            </div>
            <div style="background:#1a1a1a;padding:12px 32px;text-align:center;">
              <span style="font-size:11px;letter-spacing:2px;color:#888;">© 2026 CLOTHUB CR · SAN CARLOS · CR</span>
            </div>
          </div>
        </body>
        </html>
        """;
}
