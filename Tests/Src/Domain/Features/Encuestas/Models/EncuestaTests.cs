using Domain.Encuestas.Failures;
using Domain.Usuarios;
using FluentAssertions;

namespace Domain.Encuestas.Models {
    public class EncuestaTests {

        private readonly Encuesta _encuesta;
        private readonly Respuesta _april;
        private readonly Respuesta _masha;
        private readonly Respuesta _franchesca;


        public EncuestaTests() {
            EncuestaId id = new EncuestaId(Guid.NewGuid());
            
            _april = new Respuesta(
                new (Guid.NewGuid()),
                id,
                "April"
            );

            _masha = new Respuesta(
                new (Guid.NewGuid()),
                id,
                "masha"
            );

            _franchesca = new Respuesta(
                new (Guid.NewGuid()),
                id,
                "April"
            );

            List<Respuesta> respuestas = new List<Respuesta>(){
                _april,
                _franchesca,
                _masha  
            };

            _encuesta = Encuesta.Create(
                id,
                respuestas
            ).Value;
        }

        [Fact]
        public void Votar_Debe_RetornarSuccessResult_Cuando_UsuarioNoHaVotado(){
            UsuarioId usuarioId = new(Guid.NewGuid());
            
            var result =  _encuesta.Votar(usuarioId, _april.Id);

            result.IsSuccess.Should().BeTrue();
        }
        [Fact]
        public void Votar_Debe_AgregarVoto_A_Respuesta_Cuando_UsuarioNoHaVotado(){
            UsuarioId usuarioId = new(Guid.NewGuid());  

            int votos = _encuesta.Votos.Count;

            var result =  _encuesta.Votar(usuarioId, _april.Id);

            result.IsSuccess.Should().BeTrue();
            _encuesta.Votos.Count.Should().Be(votos + 1);
        }

        [Fact]
        public void Votar_Debe_RetornarFailureResult_Cuando_RespuestaNoExiste(){
            UsuarioId usuarioId = new(Guid.NewGuid());
            
            var result =  _encuesta.Votar(usuarioId, new(Guid.NewGuid()));

            result.Error.Should().Be(EncuestasFailures.RESPUESTA_INEXISTENTE);
        }
        [Fact]
        public void Votar_Debe_RetornarFailureResult_Cuando_UsuarioYaHaVotado(){
            UsuarioId usuarioId = new(Guid.NewGuid());
            _encuesta.Votar(usuarioId, _april.Id);

            var result =  _encuesta.Votar(usuarioId, _april.Id);

            result.Error.Should().Be(EncuestasFailures.YA_HA_VOTADO);
        }


        [Fact]
        public void HaVotado_Debe_RetornarFalso_Cuando_Usuario_NoHaVotado(){
            UsuarioId usuarioId = new(Guid.NewGuid());
            
            var result =  _encuesta.HaVotado(usuarioId);

            result.Should().BeFalse();
        }
    }
}