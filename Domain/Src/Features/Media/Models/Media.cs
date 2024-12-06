using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Medias;

public class MediaId : EntityId
{
    public MediaId(Guid value) : base(value) { }
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
abstract public class Media : HashedMedia
{
    public MediaProvider Provider { get; private set; }
    public string Url { get; private set; }
    public string MiniaturaUrl { get; private set; }

    protected Media(string hash, string path, MediaProvider provider, string url, string miniaturaUrl) : base(hash, path)
    {
        Provider = provider;
        Url = url;
        MiniaturaUrl = miniaturaUrl;
    }
}


public class YoutubeVideo : Media
{
    public YoutubeVideo(string hash, string path, string url, string miniaturaUrl) : base(hash, path, MediaProvider.Youtube, url, miniaturaUrl)
    {
    }
}

public class Imagen : Media
{
    public Imagen(string hash, string path, string url, string miniaturaUrl) : base(hash, path, MediaProvider.Imagen, url, miniaturaUrl)
    {
    }
}

public class Video : Media
{
    public Video(string hash, string path, string url, string miniaturaUrl) : base(hash, path, MediaProvider.Video, url, miniaturaUrl)
    {
    }
}
public class MediaProvider : ValueObject
{
    public string Value { get; private set; }

    private MediaProvider() {}

    static public MediaProvider Youtube => new(){
        Value = "youtube"
    };

    static public MediaProvider Imagen => new(){
        Value = "imagen"
    };

    static public MediaProvider Video => new(){
        Value = "video"
    };

    static public MediaProvider Desconocido => new(){
        Value = "desconocido"
    };

    protected override IEnumerable<object> GetAtomicValues()
    {
        throw new NotImplementedException();
    }
}