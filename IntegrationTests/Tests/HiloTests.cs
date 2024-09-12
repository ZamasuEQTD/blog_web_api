using Application.Categorias.Commands;

namespace IntegrationTests.Tests
{
    [Collection("IntegrationTesting")]
    public class HiloTests : BaseIntegrationTest
    {
        public HiloTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Postear_DebeAgregar_AgregarHilo()
        {
            var command = new CrearCategoriaCommand("nombre", []);

            await Sender.Send(command);
        }
    }
}