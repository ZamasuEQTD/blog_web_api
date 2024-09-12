using SharedKernel;

namespace Domain.Media.ValueObjects
{
    public class NetworkSource : ValueObject
    {
        public string Value { get; private set; }

        private NetworkSource(string value)
        {
            Value = value;
        }

        public static readonly NetworkSource Youtube = new NetworkSource("YTB");

        protected override IEnumerable<object> GetAtomicValues()
        {
            return new[] { Value };

        }
    }
}