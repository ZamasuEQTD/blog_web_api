using Domain.Features.Medias.Models.ValueObjects;
using SharedKernel.Abstractions;

namespace Domain.Features.Medias.Models;

public class MediaSpoileable : Entity<MediaSpoileableId>
{
    public HashedMediaId HashedMediaId { get; private set; }
    public HashedMedia HashedMedia { get; private set; }
    public bool Spoiler { get; private set; }

    private MediaSpoileable() { }
    public MediaSpoileable(HashedMediaId hashedMediaId, HashedMedia hashedMedia, bool spoiler) : base()
    {
        HashedMediaId = hashedMediaId;
        HashedMedia = hashedMedia;
        Spoiler = spoiler;
    }
}