using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Features.Comentarios.Abstractions;
using Domain.Categorias;
using Domain.Categorias.Abstractions;
using Domain.Comentarios;
using Domain.Comentarios.Services;
using Domain.Comentarios.ValueObjects;
using Domain.Core;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using Domain.Notificaciones.Abstractions;
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
        private readonly IColorService _colorService;
        

        public ComentarHiloCommandHandler(IUnitOfWork unitOfWork, IHilosRepository hilosRepository, IUserContext userContext, ICategoriasRepository categoriasRepository, INotificacionesRepository notificacionesRepository, IDateTimeProvider timeProvider, IColorService colorService)
        {
            _unitOfWork = unitOfWork;
            _hilosRepository = hilosRepository; 
            _userContext = userContext;
            _categoriasRepository = categoriasRepository;
            _timeProvider = timeProvider;
            _colorService = colorService;
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
                new Autor("Anonimo","Anon"),
                null,
                texto.Value,
                await _colorService.GenerarColor(hilo.SubcategoriaId),
                new InformacionDeComentario(
                    TagsService.GenerarTag(),
                    hilo.Configuracion.IdUnicoActivado ? TagsService.GenerarTagUnico(
                        hilo.Id,
                        new UsuarioId(_userContext.UsuarioId)
                    ) : null,
                    hilo.Configuracion.Dados ? DadosService.Generar() : null
                )
            );

            Result result = hilo.Comentar(c, _timeProvider.UtcNow);

            if (result.IsFailure) return result.Error;

            _hilosRepository.Add(c);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }

    public class ColorService : IColorService
    {
        static private readonly List<WeightValue<Colores>> _colors = [
                new WeightValue<Colores>(20,Colores.Amarillo),
                new WeightValue<Colores>(20,Colores.Azul),
                new WeightValue<Colores>(20,Colores.Rojo),
                new WeightValue<Colores>(20,Colores.Verde),
                new WeightValue<Colores>(5,Colores.Multi),
                new WeightValue<Colores>(5,Colores.Invertido),
                new WeightValue<Colores>(1,Colores.White),
            ];
        private readonly IDateTimeProvider _time;
        private readonly ICategoriasRepository _categoriasRepository;
        public ColorService(IDateTimeProvider time, ICategoriasRepository categoriasRepository)
        {
            _time = time;
            _categoriasRepository = categoriasRepository;
        }
        public async Task<Colores> GenerarColor(SubcategoriaId subcategoria)
        {
            List<WeightValue<Colores>> colors = [.._colors];

            Subcategoria _subcategoria = await _categoriasRepository.GetSubcategoria(subcategoria);
            
            if(_time.UtcNow.Hour > 22 || _time.UtcNow.Hour < 5 && _subcategoria.EsParanormal)
            {
                colors.Add(new WeightValue<Colores>(1,Colores.Black));
            }

            return colors.Pick();
        }
    }

}