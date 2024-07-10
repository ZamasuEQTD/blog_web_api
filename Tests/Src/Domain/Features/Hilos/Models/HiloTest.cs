using Domain.Hilos;
using Domain.Usuarios;

namespace Tests.Domain.Hilos {
    public class HiloTest
    {
        private readonly Hilo _hilo;
        private readonly Usuario _usuario;
        public HiloTest() {
            _usuario =  Anonimo.Create(
                new(Guid.NewGuid()),
                Username.Create("Gatubi").Value,
                "password"
            );
            _hilo = Hilo.Create(
                new Hilo.HiloId(Guid.NewGuid()),
                _usuario.Id,
                new (Guid.NewGuid()),
                null,
                "tituloo",
                "descripcion"
            );
        }

        [Fact]
        public void TestName()
        {
            // Given
        
            // When
        
            // Then
        }
    }
}