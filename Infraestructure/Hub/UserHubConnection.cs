namespace Infraestructure.Hub;

public class UserHubConnection
{
    public string? UserId { get; set; }
    public UserType UserType { get; set; }
}
public enum UserType
{
    Anonimo,
    Moderador,
}
