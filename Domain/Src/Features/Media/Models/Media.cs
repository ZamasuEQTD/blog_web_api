using SharedKernel.Abstractions;

namespace Domain.Media  {
    public class MediaId : EntityId {
        public MediaId() : base(){}
        public MediaId(Guid id) : base(id){}
    } 

    public abstract class Media : Entity<MediaId>{
        public MediaProvider Provider {get; private set;}
        protected Media():base(){}
        public Media(MediaProvider provider) : base(){
            this.Id = new(Guid.NewGuid());
            this.Provider = provider;
        }
    }

    public class Video : Media {
        public MediaProvider Previsualizacion { get; private set;}
        public Video(MediaProvider previsualizacion, MediaProvider provider) : base(provider) {
            Previsualizacion = previsualizacion;
        }
    }

    public class Imagen : Media {
        public MediaId? Thumbnail { get; private set;}

        public Imagen(MediaProvider provider) :base(provider) {

        }
    }
}