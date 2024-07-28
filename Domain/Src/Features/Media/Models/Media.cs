using System.Runtime.ConstrainedExecution;
using Domain.Media;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Media {

    public abstract class Media : Entity<MediaId>{
        public MediaProvider Provider {get; private set;}
        protected Media():base(){}
        public Media(MediaId id, MediaProvider provider) : base(id){
            this.Provider = provider;
        }
    }

    public class Youtube : Video {
        protected Youtube():base(){}

        public Youtube(MediaId id,MediaId thumbnail, MediaId previsualizacion, MediaProvider provider) : base(id, thumbnail,previsualizacion, provider) {}
    }

    public class Video : Media {
        public MediaId Previsualizacion {get;private set;}
        public MediaId Thumbnail {get; private set;}

        protected Video():base(){}
        public Video(MediaId id,MediaId thumbnail, MediaId previsualizacion, MediaProvider provider) : base(id, provider)
        {
            this.Thumbnail = thumbnail;
            this.Previsualizacion = previsualizacion;
        }
    }

    public class Imagen : Media {
        public MediaId? Thumbnail {get; private set;}

        protected Imagen():base(){}
        public Imagen(MediaId id, MediaProvider provider, MediaId? thumbnail) : base(id, provider)
        {
            Thumbnail = thumbnail;
        }
    }

    public class MediaId : EntityId {
        public MediaId() : base(){}
        public MediaId(Guid id) : base(id){}
    } 
    public static class YoutubeUtils {
        public static bool EsYoutubeVideo(string url) => false;
        public static string  GetVideoId(string url) => "";
        public static string  GetVideoThumbnailFromId(string id) => "";
        public static string  GetVideoThumbnailFromUrl(string url) => GetVideoThumbnailFromId(GetVideoId(url));
    }
}