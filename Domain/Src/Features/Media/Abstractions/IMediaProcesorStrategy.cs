using Domain.Abstractions;
using MediatR;

namespace Domain.Media.Abstractions
{

    public class MediaProcesorParams {
        public Stream Stream;
        public string Filename;
        public string ContentType;
        public string Path;
        public MediaProcesorParams(Stream stream)
        {
            Stream = stream;
        }
    }
    public interface IMediaProcesorStrategy : IStrategy<MediaProcesorParams,Media>{}
}