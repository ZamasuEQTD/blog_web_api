using Application.Abstractions.Messaging;
using Application.Medias.Abstractions;
using Domain.Hilos.ValueObjects;
using FluentValidation;

namespace Application.Hilos.Commands
{
    public class PostearHiloCommand : ICommand<Guid>
    {
        public required string Titulo { get; set; }
        public required string Descripcion { get; set; }
        public required bool Spoiler { get; set; }
        public required Guid Subcategoria { get; set; }
        public List<string> Encuesta { get; set; } = [];
        public required bool DadosActivados { get; set; }
        public required bool IdUnicoAtivado { get; set; }
        public required IFile? File { get; set; }
        public required IEmbedFile? Embed { get; set; }
    }

    public class PostearHiloCommandValidation : AbstractValidator<PostearHiloCommand>{
        public PostearHiloCommandValidation()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("El título es requerido.")
                .MinimumLength(Titulo.MIN).WithMessage($"El título debe tener al menos {Titulo.MIN} caracteres.")
                .MaximumLength(Titulo.MAX).WithMessage($"El título no puede exceder los {Titulo.MAX} caracteres.");

            RuleFor(x => x.Descripcion)
                .MaximumLength(Descripcion.MAX).WithMessage($"La descripción no puede exceder los {Descripcion.MAX} caracteres.")
                .MinimumLength(Descripcion.MIN).WithMessage($"La descripción debe tener al menos {Descripcion.MIN} caracteres.")
                .NotEmpty().WithMessage("La descripción es requerida.");

            RuleFor(x => x)
                .Must(x => x.File is not null || x.Embed is not null)
                .WithMessage("La portada es requerida");
        }
    }
}