using Domain.Media.ValueObjects;
using SharedKernel.Abstractions;

namespace Domain.Media
{
    public class MediaReference : Entity<MediaReferenceId>
    {
        public MediaId MediaId { get; private set; }
        public bool Spoiler { get; private set; }
        protected MediaReference() { }
        public MediaReference(MediaId mediaId, bool spoiler)
        {
            Id = new(Guid.NewGuid());
            MediaId = mediaId;
            Spoiler = spoiler;
        }

    }

    public class MediaReferenceId : EntityId
    {
        private MediaReferenceId() { }

        public MediaReferenceId(Guid id) : base(id) { }
    }

    public class MediaId : EntityId
    {
        private MediaId() { }
        public MediaId(Guid id) : base(id) { }
    }

    public class HashedMedia : Entity<MediaId>
    {
        public string Hash { get; private set; }
        public string Path { get; private set; }
        protected HashedMedia() { }
        protected HashedMedia(string hash, string path)
        {
            Id = new MediaId(Guid.NewGuid());
            Hash = hash;
            Path = path;
        }
    }

    public abstract class FileMedia : HashedMedia
    {
        public string FileName { get; private set; }
        public MediaSource Source { get; private set; }
        protected FileMedia() { }
        protected FileMedia(string hash, MediaSource source, string path, string fileName) : base(hash, path)
        {
            FileName = fileName;
            this.Source = source;
        }
    }

    public abstract class NetworkMedia : HashedMedia
    {
        public NetworkSource Source { get; private set; }
        protected NetworkMedia() { }
        protected NetworkMedia(string hash, string path, NetworkSource source) : base(hash, path)
        {
            Source = source;
        }
    }

    public class Imagen : FileMedia
    {
        public string Miniatura { get; private set; }
        protected Imagen() { }

        public Imagen(string hash, string path, string fileName, string miniatura) : base(hash, MediaSource.Imagen, path, fileName)
        {
            Miniatura = miniatura;
        }
    }
    public class Gif : FileMedia
    {
        public string Miniatura { get; private set; }
        public Gif(string hash, string path, string fileName, string miniatura) : base(hash, MediaSource.Imagen, path, fileName)
        {
            Miniatura = miniatura;
        }
    }
    public class Video : FileMedia
    {
        public string Previsulizacion { get; private set; }
        public string Miniatura { get; private set; }
        protected Video() { }

        public Video(string hash, string path, string fileName, string miniatura, string previsulizacion) : base(hash, MediaSource.Video, path, fileName)
        {
            Miniatura = miniatura;
            Previsulizacion = previsulizacion;
        }


    }

    public class YoutubeVideo : NetworkMedia
    {
        public string Miniatura { get; private set; }
        public string Previsulizacion { get; private set; }
        public YoutubeVideo()
        {

        }
        public YoutubeVideo(string hash, string path, string miniatura, string previsulizacion) : base(hash, path, NetworkSource.Youtube)
        {
            Miniatura = miniatura;
            Previsulizacion = previsulizacion;
        }
    }
}
