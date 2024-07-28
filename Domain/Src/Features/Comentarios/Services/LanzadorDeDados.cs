using Domain.Comentarios.ValueObjects;
using Domain.Common.Abstractions;

namespace Domain.Comentarios.Services {
    public class LanzadorDeDados  {
        public IRandomGeneratorService _random;
        public LanzadorDeDados(IRandomGeneratorService random)
        {
            _random = random;
        }

        public Dados TirarDados() => Dados.Create(_random.GenerarInt(Dados.MIN,Dados.MIN)).Value;
    }
}