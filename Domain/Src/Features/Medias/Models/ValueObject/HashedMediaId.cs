using SharedKernel.Abstractions;

namespace Domain.Features.Medias.Models.ValueObjects;

public class HashedMediaId : EntityId
{
    public HashedMediaId(Guid value) : base(value) { }
}
