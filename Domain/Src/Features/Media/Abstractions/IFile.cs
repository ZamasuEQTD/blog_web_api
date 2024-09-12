using Domain.Media.ValueObjects;

namespace Domain.Media.Abstractions
{
    public interface IFile
    {
        public string FileName { get; }
        public Stream Stream { get; }
        public string ContentType { get; }
        public string Extension { get; }
        public FileType Type { get; }
    }
}