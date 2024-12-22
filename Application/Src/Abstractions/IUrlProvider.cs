namespace Application.Abstractions;

public interface IUrlProvider {
    string BaseUrl {get;}
    string Media {get;}
    string Files {get;}
    string Thumbnail {get;}
    string Previsualizacion {get;}
}