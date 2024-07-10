using Domain.Hilos;
using Domain.Hilos.Abstractions;
using Domain.Usuarios;
using FluentAssertions;

namespace Tests.Domain.Usuarios {

    public class ModeradorTest {
        private readonly Moderador moderador;
        private readonly Hilo _hilo;
        public ModeradorTest(){
            moderador = new Moderador(new(Guid.NewGuid()),Username.Create("Gatubiiiii").Value,"password");

            _hilo = Hilo.Create(
                new Hilo.HiloId(Guid.NewGuid()),
                moderador.Id,
                new (Guid.NewGuid()),
                null,
                "tituloo",
                "descripcion"
            );
        }

        [Fact]
        public void Eliminar_Hilo_DebeRetornarSuccessResult_Cuando_HiloNoEstaEliminado() {
            var result = moderador.EliminarHilo(new HilosManager(_hilo));

            result.IsSuccess.Should().BeTrue();
        }        
    }
    

}