using Application.Categorias.Commands;
using Application.Categorias.Queries;

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

            var query = new GetCategoriasQuery();

            var res = await Sender.Send(query);

            if (res.IsFailure) { }
        }
    }
}