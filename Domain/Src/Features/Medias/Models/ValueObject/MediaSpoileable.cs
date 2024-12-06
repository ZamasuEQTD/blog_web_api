using SharedKernel.Abstractions;

namespace Domain.Features.Medias.Models.ValueObjects;

public class MediaSpoileableId : EntityId
{
    public MediaSpoileableId(Guid value) : base(value) { }
}