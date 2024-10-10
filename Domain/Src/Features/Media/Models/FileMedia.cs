using Domain.Media.ValueObjects;

namespace Domain.Media
{
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
}