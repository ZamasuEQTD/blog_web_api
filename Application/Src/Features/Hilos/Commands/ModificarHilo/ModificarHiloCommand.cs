using System.Text.Json.Serialization;
using Application.Abstractions.Messaging;

namespace Application.Hilos.Commands.ModificarHilo;

public class ModificarHiloCommand : ICommand
{
    public Guid Id { get; set; }
    [JsonPropertyName("subcategoria_id")]
    public Guid SubcategoriaId { get; set; }
}