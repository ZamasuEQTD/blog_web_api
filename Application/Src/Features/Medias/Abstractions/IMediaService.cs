
using Domain.Features.Medias.Models;

namespace  Application.Medias.Abstractions;

public interface IMediaService
{
    Task<Media> Create(string path);
}
