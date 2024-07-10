using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Hilos.Commands
{
    public class CrearHiloCommand : ICommand< Guid >
    {
        public string Titulo {get; set;}
        public string Descripcion {get; set;}
        public Stream Portada {get; set;}
        public string ContentType {get; set;}
        public bool EsSpoiler {get;set;}
        public List<string>? Encuesta {get;set;}
        public CrearHiloCommand(string titulo, string descripcion)
        {
            this.Titulo = titulo;
            this.Descripcion = descripcion;
        }
    }
}