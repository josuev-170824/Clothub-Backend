using FluentValidation;

namespace Clothub.Application.Auth.Commands.LoginWithEmail;

public class LoginWithEmailCommandValidator : AbstractValidator<LoginWithEmailCommand> {
    public LoginWithEmailCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(255).WithMessage("El email no es válido");
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8).MaximumLength(255).WithMessage("La contraseña debe tener entre 8 y 255 caracteres");
    }
}
