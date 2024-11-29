using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Categorias;
using Domain.Categorias.Abstractions;
using Domain.Comentarios;
using Domain.Comentarios.Services;
using Domain.Comentarios.ValueObjects;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using Domain.Usuarios;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Application.Comentarios.Commands
{
    public class ComentarHiloCommandHandler : ICommandHandler<ComentarHiloCommand>
    {
        private readonly IHilosRepository _hilosRepository;
        private readonly ICategoriasRepository _categoriasRepository;
        private readonly IUserContext _userContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _timeProvider;
        
        public ComentarHiloCommandHandler(IUnitOfWork unitOfWork, IHilosRepository hilosRepository, IUserContext userContext, ICategoriasRepository categoriasRepository, IDateTimeProvider timeProvider)
        {
            _unitOfWork = unitOfWork;
            _hilosRepository = hilosRepository;
            _userContext = userContext;
            _categoriasRepository = categoriasRepository;
            _timeProvider = timeProvider;
        }

        public async Task<Result> Handle(ComentarHiloCommand request, CancellationToken cancellationToken)
        {
            Hilo? hilo = await _hilosRepository.GetHiloById(new HiloId(request.Hilo));

            if (hilo is null) return HilosFailures.NoEncontrado;

            if (!hilo.Activo) return HilosFailures.Inactivo;

            Result<Texto> texto = Texto.Create(request.Texto);

            if (texto.IsFailure) return texto.Error;

            Comentario c = new Comentario(
                hilo.Id,
                new UsuarioId(_userContext.UsuarioId),
                new Autor(_userContext.Rango == Rango.Moderador ? _userContext.Username : "Anonimo", _userContext.Rango.ToRangoDeUsuario()),
                null,
                texto.Value,
                ColorService.GenerarColor(
                    hilo.Categoria,
                    await _categoriasRepository.GetSubcategoriasParanormales(),
                    _timeProvider.UtcNow
                ),
                new InformacionDeComentario(
                    TagsService.GenerarTag(),
                    hilo.Configuracion.IdUnicoActivado ? TagsService.GenerarTagUnico(
                        hilo.Id,
                        new UsuarioId(_userContext.UsuarioId)
                    ) : null,
                    hilo.Configuracion.Dados ? DadosService.Generar() : null
                )
            );

            hilo.Comentar(c, _timeProvider.UtcNow);

            _hilosRepository.Add(c);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
    public static class RangoExtension
    {
        public static RangoDeUsuario ToRangoDeUsuario(this Rango rango)
        {
            if (rango == Rango.Moderador) return RangoDeUsuario.Moderador;
            if (rango == Rango.Anonimo) return RangoDeUsuario.Anonimo;
            if (rango == Rango.Owner) return RangoDeUsuario.Owner;
            throw new ArgumentException("Rango no válido");
        }
    }
}