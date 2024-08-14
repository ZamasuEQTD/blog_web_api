using Domain.Abstractions;
using MediatR;

namespace Domain.Medias.Abstractions
{

    public class MediaProcesorParams
    {
        public required Stream Stream;
        public required string Filename;
        public required string ContentType;
        public required string Path;
    }
    public interface IMediaProcesorStrategy : IStrategy<MediaProcesorParams, Media> { }

    public interface IUrlMediaProcesorStrategy : IStrategy<string, Media>
    {
    }
}