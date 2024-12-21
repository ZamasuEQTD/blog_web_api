using SharedKernel.Abstractions;

namespace Domain.Encuestas.DomainEvents;

public class EncuestaVotadaDomainEvent : IDomainEvent
{
    public EncuestaId EncuestaId {get; private set;}
    public RespuestaId RespuestaId {get; private set;}

    public EncuestaVotadaDomainEvent(EncuestaId encuestaId, RespuestaId respuestaId)
    {
        RespuestaId = respuestaId;
        EncuestaId = encuestaId;
    }
}