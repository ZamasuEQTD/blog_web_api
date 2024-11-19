using Dapper;
using Domain.Comentarios.ValueObjects;

namespace Application.Comentarios.Queries.GetComentariosDeHilo.Builder;
    public interface IRule<E> {
        public bool IsRuleApplicable(E input);
        public void ApplyRule(E input);
    }

    public class FiltrarPorHiloRule : IRule<ComentariosBuilder> {
        private readonly Guid? _hilo;
        public FiltrarPorHiloRule(Guid? hilo) => _hilo = hilo;
        public bool IsRuleApplicable(ComentariosBuilder input) => _hilo is not null;
        public void ApplyRule(ComentariosBuilder input) =>  input.PorHilo( (Guid)_hilo! );
    }

    public class FiltrarPorCreacionRule : IRule<ComentariosBuilder> {
        private readonly DateTime? _creacion;
        public FiltrarPorCreacionRule(DateTime? creacion) => _creacion = creacion;
        public bool IsRuleApplicable(ComentariosBuilder input) => _creacion is not null;
        public void ApplyRule(ComentariosBuilder input) => input.FiltrarCreadosMenores((DateTime) _creacion!);
    }

    public class FiltrarPorIdsRule : IRule<ComentariosBuilder> {
        private readonly List<string> _ids;
        public FiltrarPorIdsRule(List<string> ids) => _ids = ids;
        public bool IsRuleApplicable(ComentariosBuilder input) => _ids.Count > 0;
        public void ApplyRule(ComentariosBuilder input) => input.EliminarPorIds(_ids);
    }

    public class FiltrarPorStatusRule : IRule<ComentariosBuilder> {
        private readonly ComentarioStatus _status;

        public FiltrarPorStatusRule(ComentarioStatus status) => _status = status;
        public bool IsRuleApplicable(ComentariosBuilder input) => _status is not null;
        public void ApplyRule(ComentariosBuilder input) => input.PorStatus(_status);
    }
    
    public class SoloDestacadosRule : IRule<ComentariosBuilder> {
        public bool IsRuleApplicable(ComentariosBuilder input) => true;
        public void ApplyRule(ComentariosBuilder input) => input.SoloDestacados();
    }

    public class OrdenarPorCreacionRule : IRule<ComentariosBuilder> {
        public bool IsRuleApplicable(ComentariosBuilder input) => true;
        public void ApplyRule(ComentariosBuilder input) => input.OrdenarPorCreacion();
    }

    public class ComentariosBuilder
    {
        public SqlBuilder builder = new SqlBuilder();
        
        public ComentariosBuilder PorHilo(Guid hilo){
            builder.Where("comentario.hilo_id = @hilo", new
            {
                hilo = hilo
            });

            return this;
        }

        public ComentariosBuilder OrdenarPorCreacion(){
            builder.OrderBy("comentario.created_at DESC");

            return this;
        }

        public ComentariosBuilder SoloDestacados(){
            builder.Where("EXISTS (SELECT 1 FROM comentarios_destacados destacado WHERE destacado.id = comentario.id)");

            return this;
        }

        public ComentariosBuilder PorStatus(ComentarioStatus status){
            builder.Where("comentario.status = @status", new
            {
                status = status.Value
            });

            return this;
        }

        public ComentariosBuilder FiltrarCreadosMenores(DateTime creacion){
            builder.Where("comentario.created_at < @creacion ::Date", new
            {
                creacion 
            });

            return this;
        }

        public ComentariosBuilder EliminarPorIds(List<string> ids){
            builder.Where("comentario.id NOT IN @ids", new
            {
                ids = ids
            });

            return this;
        }
    }   