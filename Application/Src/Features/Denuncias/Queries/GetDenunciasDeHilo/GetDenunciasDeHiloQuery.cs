using Application.Abstractions.Messaging;

namespace Application.Denuncias.Queries.GetDenunciasDeHilo;

public class GetDenunciasDeHiloQuery : IQuery<IEnumerable<GetDenunciaResponse>>
{
    public Guid HiloId { get; set; }
}   

public class GetDenunciaResponse {
    public Guid Id { get; set; }
    public string Motivo { get; set; }
    public string Usuario { get; set; }
    public DateTime Fecha { get; set; }
    public string Hilo { get; set; }
    public string Contenido { get; set; }
}

public class GetHiloDenunciaResponse {
    public Guid Id { get; set; }
    public string Titulo { get; set; }
}