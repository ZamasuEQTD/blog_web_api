using Microsoft.AspNetCore.SignalR;

namespace Infraestructure.Hubs.Abstractions;

public interface IUserHubContext {
    bool IsAuthenticated {get;}
    string ConnectionId {get;} 
    Guid UserId {get;}
    UserType Role {get;}
}

public enum UserType
{
    Anonimo,
    Moderador,
}

public class UserHubContext(HubCallerContext  context) : IUserHubContext, IEquatable<UserHubContext>
{
    public bool IsAuthenticated => context.User!.Identity?.IsAuthenticated ?? false;
    public string ConnectionId => context.ConnectionId;
    public Guid UserId => Guid.TryParse(context.UserIdentifier, out var id) ? id : throw new ArgumentException("User id is not a valid Guid");
    public UserType Role { get; set; }

    public override int GetHashCode() => ConnectionId.GetHashCode();

    public bool Equals(UserHubContext? other)
    {
        if (other is null) return false;

        if (ReferenceEquals(this, other)) return true;
        
        return ConnectionId == other.ConnectionId;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        
        if (obj.GetType() != GetType()) return false;

        return Equals(obj as UserHubContext);
    }
}