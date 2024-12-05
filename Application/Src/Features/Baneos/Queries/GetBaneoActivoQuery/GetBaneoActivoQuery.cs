using Application.Abstractions.Messaging;

namespace Application.Baneos.Queries
{
    public class GetBaneoActivoQuery : IQuery<GetBaneoResponse?> { }

    public class GetBaneoResponse
    {
        public required Guid Id { get; set; }
        public required string Moderador { get; set; }
        public required string Razon { get; set; }
        public required string? Mensaje { get; set; }
        public required DateTime? Concluye { get; set; }
    }
}