using Application.Abstractions.Messaging;

namespace Application.Encuesta.VotarRespuesta;

public class VotarRespuestaCommand : ICommand
{
    public Guid EncuestaId { get; set; }
    public Guid RespuestaId { get; set; }
}