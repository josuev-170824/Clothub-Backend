using FluentValidation;

namespace Clothub.Application.Auth.Commands.VerificarEmail;

public class VerificarEmailCommandValidator : AbstractValidator<VerificarEmailCommand>
{
    public VerificarEmailCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Codigo).NotEmpty().Matches(@"^\d{6}$").WithMessage("El código debe tener 6 dígitos.");
    }
}
