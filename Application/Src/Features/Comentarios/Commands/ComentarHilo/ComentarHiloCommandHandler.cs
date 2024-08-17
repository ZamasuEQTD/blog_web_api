using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Hilos;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using SharedKernel;

namespace Application.Comentarios.Commands
{
    public class ComentarHiloCommandHandler : ICommandHandler<ComentarHiloCommand>
    {
        private readonly IHilosRepository _hilosRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ComentarHiloCommandHandler(IUnitOfWork unitOfWork, IHilosRepository hilosRepository)
        {
            _unitOfWork = unitOfWork;
            _hilosRepository = hilosRepository;
        }

        public Task<Result> Handle(ComentarHiloCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}