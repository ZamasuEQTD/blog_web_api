using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Encuestas;
using Domain.Encuestas;
using Domain.Encuestas.Abstractions;
using Domain.Hilos;
using Domain.Hilos.Abstractions;

namespace Application.Hilos.Commands
{
    public class VotarEncuestaCommandHandler : ICommandHandler<VotarEncuestaCommand> {
        private readonly IUserContext _context;
        private readonly IHilosRepository _hilosRepository;
        private readonly IEncuestasRepository _encuestasRepository;
        private readonly IUserContext _user;
        private readonly IUnitOfWork _unitOfWork;

        public VotarEncuestaCommandHandler(IUnitOfWork unitOfWork, IEncuestasRepository encuestasRepository, IHilosRepository hilosRepository, IUserContext context)
        {
            _unitOfWork = unitOfWork;
            _encuestasRepository = encuestasRepository;
            _hilosRepository = hilosRepository;
            _context = context;
        }

        public async Task Handle(VotarEncuestaCommand request, CancellationToken cancellationToken){ 
            Hilo? hilo = await _hilosRepository.GetHiloById(new (request.Hilo));
            
            if (hilo is null) throw new HiloNoEncontrado();

            Encuesta encuesta = await _encuestasRepository.GetEncuestaById(hilo.Encuesta!);

            hilo.Votar(
                encuesta,
                new (_user.UsuarioId),
                new (request.Respuesta)
            );

            await _unitOfWork.SaveChangesAsync();
        }
    }
}