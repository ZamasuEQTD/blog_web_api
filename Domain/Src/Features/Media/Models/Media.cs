using SharedKernel.Abstractions;

namespace Domain.Medias  {
    public class MediaId : EntityId {
        public MediaId() : base(){}
        public MediaId(Guid id) : base(id){}
    } 

    public abstract class Media : Entity<MediaId>{
        public string? Miniatura {get; private set;}
        public string Path {get; private set;}
        protected Media():base(){}
        protected Media(string path,string? miniatura) : base(){
            this.Id = new(Guid.NewGuid());
            this.Path = path;
            this.Miniatura = miniatura;
        }
    }

    public class Video : Media {
        public string Previsualizacion { get; private set;}
        public Video(string path, string? miniatura,string previsualizacion) : base(path,miniatura) {
            this.Previsualizacion = previsualizacion;
        }
    }

    public class YoutubeVideo : Video {
        public YoutubeVideo(string path, string? miniatura, string previsualizacion) : base(path, miniatura, previsualizacion){}
    }


    public class Imagen : Media { 
        public Imagen(string path, string? miniatura) : base(path,miniatura) {}
    }
    
    public class Audio : Media
    {
        public string Nombre { get; private set;}

        public Audio(string path, string nombre) : base(path,null) {
            Nombre = nombre;
        }
    }
}