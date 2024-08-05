using Domain.Usuarios.ValueObjects;

namespace Domain.Usuarios.Abstractions
{
    public interface IPasswordHasher
    {
        public string Hash(Password password);
        public bool Verify(Password password,string hashedPassword);
    }
}