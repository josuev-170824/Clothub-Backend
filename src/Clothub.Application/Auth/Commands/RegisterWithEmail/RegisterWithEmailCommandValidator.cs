using FluentValidation;

namespace Clothub.Application.Auth.Commands.RegisterWithEmail;

public class RegisterWithEmailCommandValidator : AbstractValidator<RegisterWithEmailCommand>
{
    public RegisterWithEmailCommandValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100).WithMessage("El nombre debe tener menos de 100 caracteres");
        RuleFor(x => x.Apellidos).NotEmpty().MaximumLength(100).WithMessage("Los apellidos deben tener menos de 100 caracteres")    ;
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(255).WithMessage("El email no es válido");
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8).MaximumLength(255).WithMessage("La contraseña debe tener entre 8 y 255 caracteres");
    }
}