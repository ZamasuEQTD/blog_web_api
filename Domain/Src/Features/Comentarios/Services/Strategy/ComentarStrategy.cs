using Domain.Comentarios;
using Domain.Comentarios.Services;
using Domain.Comentarios.Validators;
using Domain.Common.Abstractions;
using Domain.Hilos;
using Domain.Usuarios;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Comentarios.Services.Strategies {
    public class InformacionDeComentarioGenerador {
        private readonly TagGenerator _tagGenerator;
        private readonly LanzadorDeDados _lanzadorDeDados; 
        private readonly UsuarioId _autorId;

        public InformacionDeComentarioGenerador(LanzadorDeDados lanzadorDeDados, TagGenerator tagGenerator, UsuarioId autorId)
        {
            _lanzadorDeDados = lanzadorDeDados;
            _tagGenerator = tagGenerator;
            _autorId = autorId;
        }

        public InformacionComentario Generar(Hilo hilo){
            return new InformacionComentario(
                _tagGenerator.Generar(),
                hilo.Configuracion.Dados ? _lanzadorDeDados.TirarDados() : null,
                hilo.Configuracion.IdUnicoActivado ? new TagUnicoGenerador(
                    new RandomTextGenerator(
                        new SeededRandomGenerator((hilo.Id.ToString() + _autorId.ToString()).GetHashCode())
                    )
                ).Generar() : null
            );
        }
    }

    public interface  IComentarStrategy {
        public  Result<Comentario> Comentar(
            UsuarioId usuario,
            string _texto,
            Hilo _hilo
        );
    }

    public class AnonimoComentarStrategy : IComentarStrategy {
        public static readonly int MAX = 250;
        public static readonly int MIN = 20;

        private readonly InformacionDeComentarioGenerador informacionGenerador;

        public AnonimoComentarStrategy(InformacionDeComentarioGenerador informacionGenerador)
        {
            this.informacionGenerador = informacionGenerador;
        }

        public  Result<Comentario> Comentar(UsuarioId usuario, string _texto, Hilo _hilo)
        {
            ComentarStrategyAnonimoValidation validation = new ComentarStrategyAnonimoValidation(_hilo,_texto);
            
            var result = validation.Validar();

            if(result.IsFailure)return result.Error;

            return _hilo.Comentar(
                usuario,
                informacionGenerador.Generar(_hilo),
                _texto
            );
        }
    }

    public class ModeradorComentarStrategy : IComentarStrategy{

        private readonly InformacionDeComentarioGenerador informacionGenerador;

        public ModeradorComentarStrategy(InformacionDeComentarioGenerador informacionGenerador)
        {
            this.informacionGenerador = informacionGenerador;
        }

        public  Result<Comentario> Comentar(UsuarioId usuario, string _texto, Hilo _hilo)
        {
            return _hilo.Comentar(
                usuario,
                informacionGenerador.Generar(_hilo),
                _texto
            );
        }
    }

    public interface IComentarStrategyValidation {
        public Result Validar();
    }

    public class ComentarStrategyAnonimoValidation : IComentarStrategyValidation {
        private readonly string _texto;
        private readonly Hilo _hilo;

        public ComentarStrategyAnonimoValidation(Hilo hilo, string texto)
        {
            _hilo = hilo;
            _texto = texto;
        }

        public Result Validar() {
            ValidationHandler handler = new NoPuedeTaguearMasComentariosQueLaPermitida(_texto);
            return handler.Handle();
        }
    }
}