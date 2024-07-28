using Domain.Comentarios.Failures;
using Domain.Comentarios.Services;
using Domain.Comentarios.Validators;
using Domain.Comentarios.ValueObjects;
using Domain.Hilos;
using Domain.Hilos.Validators;
using Domain.Usuarios;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Comentarios {
    public class Comentario : Entity<ComentarioId> {
        public UsuarioId AutorId { get;private set; }
        public HiloId HiloId { get; private set;}
        public InformacionComentario Informacion { get; private set; }
        public ComentarioStatus Status { get; private set; }	
        public string Texto { get; private set; }
        public bool Destacado {get; private set; }
        private Comentario(){}
        public Comentario(
            ComentarioId id,
            UsuarioId autorId,
            HiloId hiloId,
            InformacionComentario informacion,
            string texto
        ) : base(id) {
            Informacion = informacion;
            Texto = texto;
            HiloId = hiloId;
            AutorId = autorId;
            Status = ComentarioStatus.Activo;
            Destacado = false;
        }

        static public  Comentario  Create(
            ComentarioId id,
            UsuarioId autor,
            Hilo hilo,
            InformacionComentario informacion,
            string texto
        ){
           
            return new Comentario(
                id,
                autor,
                hilo.Id,
                informacion,
                texto
            );
        }

        public Result Eliminar(Hilo hilo){
            var handle = new HiloDebeEstarActivoValidator(hilo);
            
            var result = handle.Handle();

            if(result.IsFailure) return result.Error;

            Status = ComentarioStatus.Eliminado;

            return Result.Success();
        }
        
        public Result Destacar(Hilo hilo){
            var handler = new HiloDebeEstarActivoValidator(hilo);

            var result = handler.Handle();

            if (result.IsFailure) return result.Error;

            this.Destacado = !Destacado;
            
            return Result.Success();
        }

        public enum ComentarioStatus
        {
            Activo,
            Eliminado
        }
    }

    public class InformacionComentario {
        public Tag Tag { get; private set; }
        public Dados? Dados {get;private set;}
        public TagUnico? TagUnico{ get;private set; }

        private InformacionComentario(){}

        public InformacionComentario(Tag tag, Dados? dados,TagUnico? tagUnico)
        {
            this.Tag = tag;
            this.Dados = dados;
            this.TagUnico = tagUnico;
        }
    }
}