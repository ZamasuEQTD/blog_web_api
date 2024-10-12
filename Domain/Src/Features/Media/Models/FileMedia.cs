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
            Source = source;
        }
    }

    public class Imagen : FileMedia
    {
        protected Imagen() { }

        public Imagen(string hash, string path, string fileName) : base(hash, MediaSource.Imagen, path, fileName)
        {
        }
    }
    public class Gif : FileMedia
    {
        public Gif(string hash, string path, string fileName) : base(hash, MediaSource.Imagen, path, fileName)
        {
        }
    }

    public class Video : FileMedia
    {
        protected Video() { }
        public Video(string hash, string path, string fileName) : base(hash, MediaSource.Video, path, fileName)
        {
        }
    }
}