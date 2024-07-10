using System.Security.Cryptography.X509Certificates;
using Domain.Usuarios;
using Domain.Usuarios.Failures;
using FluentValidation;

namespace Application.Usuarios.Commands
{
    public class RegistroCommandValidator : AbstractValidator<RegistroCommand>
    {
        public RegistroCommandValidator()
        {
            RuleFor(x=> x.Username).NotEmpty().WithMessage("Se requiere username");
            RuleFor(x=> x.Username).Must(u=> u.Length > Username.MIN_LENGTH && u.Length < Username.MAXIMO_LENGTH).WithMessage(UsernameFailures.LARGO_INVALIDO.Descripcion);

            RuleFor(x=> x.Password).NotEmpty().WithMessage("Se requiere una contraseña");
        }
    }
}