using Domain.Categorias;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using Domain.Hilos.ValueObjects;
using Domain.Usuarios;
using NSubstitute;

namespace Tests.Application.Hilos.Commands
{
    public abstract class HiloBaseCommand : BaseCommand
    {
        public Hilo _hilo;
        public IHilosRepository _hilosRepository;

        public HiloBaseCommand()
        {
            _hilosRepository = Substitute.For<IHilosRepository>();

            _hilo = new Hilo(
                            Titulo.Create("A que huele?").Value,
                            Descripcion.Create("Eso.Debatan").Value,
                            new UsuarioId(Guid.NewGuid()),
                            new SubcategoriaId(Guid.NewGuid()),
                            null,
                            new ConfiguracionDeComentarios(
                                true,
                                true
                            )
                        );
            _hilosRepository.GetDenuncias(_hilo.Id).Returns(Task.FromResult<List<DenunciaDeHilo>>([]));

        }
    }
}