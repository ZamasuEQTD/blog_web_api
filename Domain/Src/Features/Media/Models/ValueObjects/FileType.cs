using SharedKernel;

namespace Domain.Media.ValueObjects
{
    public class FileType : ValueObject
    {
        public string Value { get; private set; }

        public FileType(string value)
        {
            Value = value;
        }
        static public FileType Imagen = new FileType("Imagen");
        static public FileType Gif = new FileType("gif");
        static public FileType Video = new FileType("video");
        static public FileType Desconocido = new FileType("Desconocido");
        protected override IEnumerable<object> GetAtomicValues()
        {
            return new[] { Value };
        }
    }
}