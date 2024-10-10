using Application.Abstractions.Messaging;
using static Domain.Usuarios.Usuario;

namespace Application.Hilos.Queries
{
    public class GetPortadasHomeQuery : IQuery<List<GetPortadaHomeResponse>>
    {
        public string? Titulo { get; set; }
        public DateTime? UltimoBump { get; set; }
        public Guid? Categoria { get; set; }
    }
    public class Portada
    {
        public Guid Id { get; set; }
        public Guid Autor { get; set; }
        public Guid? Encuesta { get; set; }
        public string Titulo { get; set; }
        public string Categoria { get; set; }
        public bool Spoiler { get; set; }
        public bool DadosActivos { get; set; }
        public bool IdUnicoActivado { get; set; }
        public Archivo Archivo { get; set; }
    }
    public class Archivo
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
        public string Miniatura { get; set; }
        public string TipoDeArchivo { get; set; }
    }
    public class GetPortadaHomeResponse
    {
        public Guid Id { get; set; }
        public GetPortadaMiniaturaHomeResponse Miniatura { get; set; }
        public GetPortadaBanderasHomeResponse Banderas { get; set; }
        public bool Destacado { get; set; }
        public string Titulo { get; set; }
        public string Categoria { get; set; }
        public Guid? Autor { get; set; }
    }

    public class GetPortadaBanderasHomeResponse
    {
        public bool DadosActivos { get; set; }
        public bool TieneEncuesta { get; set; }
        public bool IdUnicoActivado { get; set; }
    }

    public class GetPortadaMiniaturaHomeResponse
    {
        public bool EsSpoiler { get; set; }
        public string Url { get; set; }
    }

    static class PortadasMapper
    {
        static public List<GetPortadaHomeResponse> ToResponses(List<Portada> portadas, RangoDeUsuario? rango, bool destacado) => portadas.Select(x => ToResponse(x, rango, destacado)).ToList();
        static public GetPortadaHomeResponse ToResponse(Portada portada, RangoDeUsuario? rango, bool destacado)
        {
            return new GetPortadaHomeResponse()
            {
                Id = portada.Id,
                Autor = rango is not null && rango == RangoDeUsuario.Moderador ? portada.Autor : null,
                Categoria = portada.Categoria,
                Titulo = portada.Titulo,
                Destacado = destacado,
                Banderas = new GetPortadaBanderasHomeResponse()
                {
                    DadosActivos = portada.DadosActivos,
                    IdUnicoActivado = portada.IdUnicoActivado,
                    TieneEncuesta = portada.Encuesta is not null
                },
                Miniatura = new GetPortadaMiniaturaHomeResponse()
                {
                    EsSpoiler = portada.Spoiler,
                    Url = ArchivoMapper.ToMiniaturaUrl(portada.Archivo)
                }
            };
        }
    }

    static class ArchivoMapper
    {

        static public string ToMiniaturaUrl(Archivo archivo)
        {

            if (archivo.TipoDeArchivo == "youtube") return archivo.Miniatura;

            return $"/media/backgrounds/{archivo.Id}";
        }
    }
}
