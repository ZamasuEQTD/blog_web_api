using Application.Medias.Abstractions;
using Domain.Features.Medias.Models;

namespace Application.Features.Medias.Abstractions
{
    public interface IEmbedService
    {
        Task<Media> Create(IEmbedFile url);
    }
}