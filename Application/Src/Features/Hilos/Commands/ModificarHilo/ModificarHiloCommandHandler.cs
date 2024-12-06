using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Categorias;
using Domain.Comentarios;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using MediatR;
using SharedKernel;

namespace Application.Hilos.Commands.ModificarHilo;

public class ModificarHiloCommandHandler : ICommandHandler<ModificarHiloCommand>
{
    private readonly IHilosRepository _hiloRepository;
    private readonly IUnitOfWork _unitOfWork;
    public ModificarHiloCommandHandler(IHilosRepository hiloRepository, IUnitOfWork unitOfWork)
    {
        _hiloRepository = hiloRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(ModificarHiloCommand command, CancellationToken cancellationToken)
    {
        var hilo = await _hiloRepository.GetHiloById(new HiloId(command.Id));

        if (hilo is null) return HilosFailures.NoEncontrado;

        hilo.ModificarSubcategoria(new SubcategoriaId(command.SubcategoriaId));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}