using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Hilos.Commands
{
    public class CrearHiloCommand : ICommand< Guid >
    {
        public string Titulo {get; set;}
        public string Descripcion {get; set;}
        public IFileProvider Portada {get; set;}
        public bool EsSpoiler {get;set;}
        public List<string> Encuesta {get;set;}
        public ConfiguracionDeComentariosCommand Configuracion {get;set;}
        public CrearHiloCommand(string titulo, string descripcion, List<string>? encuesta, ConfiguracionDeComentariosCommand configuracion, IFileProvider portada)
        {
            this.Titulo = titulo;
            this.Descripcion = descripcion;
            this.Encuesta = encuesta ?? [];
            this.Configuracion = configuracion;
            Portada = portada;
        }

        public class ConfiguracionDeComentariosCommand {
            public bool Dados {get;set;}
            public bool TagUnico {get; set;}
        }
    }
}