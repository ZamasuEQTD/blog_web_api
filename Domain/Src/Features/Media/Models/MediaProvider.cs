using SharedKernel.Abstractions;

namespace Domain.Media
{
    public class MediaProvider {
        public string Path { get; private set;}
        public ProviderSource Source { get; private set;}
        protected MediaProvider() { }
        private MediaProvider(string path,ProviderSource source) {
            this.Path = path;
            this.Source = source;
        }

        static public MediaProvider Network (string url) => new  MediaProvider(url,ProviderSource.Network);
        static public MediaProvider File (string url) => new  MediaProvider(url,ProviderSource.File);

        public enum ProviderSource {
            File,
            Network
        }
    }
     

    public class MediaProviderId : EntityId
    {
        public MediaProviderId() : base(){}

        public MediaProviderId(Guid id) : base(id) {}
    }
}