using SharedKernel;

namespace Domain.Media.ValueObjects
{
    public class MediaSource : ValueObject
    {
        public string Value { get; private set; }
        private MediaSource(string value)
        {
            Value = value;
        }
        public static readonly MediaSource Audio = new MediaSource("audio");
        public static readonly MediaSource Video = new MediaSource("video");
        public static readonly MediaSource Imagen = new MediaSource("imagen");
        public static readonly MediaSource Youtube = new MediaSource("ytb");
        public static readonly MediaSource Desconocido = new MediaSource("desconocido");

        protected override IEnumerable<object> GetAtomicValues()
        {
            return new[] { Value };
        }
    }
}