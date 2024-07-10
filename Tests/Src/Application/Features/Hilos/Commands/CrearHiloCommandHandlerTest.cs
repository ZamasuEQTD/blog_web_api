using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Hilos.Commands;
using Domain.Hilos.Abstractions;
using Domain.Media;
using Domain.Media.Abstractions;
using Domain.Usuarios;
using FluentAssertions;
using NSubstitute;

namespace Tests.Application.Hilos.Commands {
    public class CrearHiloCommandHandlerTest
    {
        private readonly IHilosRepository _hilosRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;
        private readonly IMediasRepository _mediasRepository;
        private readonly CrearHiloCommandHandler _handler;
        public CrearHiloCommandHandlerTest()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _hilosRepository = Substitute.For<IHilosRepository>();
            _userContext = Substitute.For<IUserContext>();
            _mediasRepository = Substitute.For<IMediasRepository>();
        }
        [Fact]
        public async Task Handle_Debe_RetornarFailureResult_Cuando_TituloEsInvalido()
        {
            var command =_handler;
            _userContext.Rango.Returns(Usuario.RangoDeUsuario.Anonimo);

            var result = await command.Handle(new CrearHiloCommand(
                "",
                ""
            ),default);
        }

        [Fact]
        public async Task Handle_Debe_RetornarFailureResult_Cuando_DescripcionEsInvalida()
        {
            var command  = _handler;

            _userContext.Rango.Returns(Usuario.RangoDeUsuario.Anonimo);

            var result = await command.Handle(new CrearHiloCommand(
                "A que huele????",
                ""
            )
            ,default);
        }
    }
}