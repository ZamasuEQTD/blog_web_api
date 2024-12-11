namespace Application.Medias.Abstractions;

public interface IEmbedFile  { 
    public string Url { get; }
    public string Domain { get; }
    public EmbedType Type { get; }

}
public enum EmbedType {
    Youtube,
    Desconocido
}
