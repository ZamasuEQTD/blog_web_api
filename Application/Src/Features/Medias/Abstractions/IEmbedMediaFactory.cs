using Application.Medias.Abstractions;

namespace Application.Features.Medias.Abstractions;

public interface IEmbedMediaFactory
{
    IEmbedService Create(EmbedType file);
}