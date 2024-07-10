using Domain.Usuarios.Abstractions;
using Domain.Usuarios.ValueObjects;

namespace Infraestructure.Usuarios
{
     public class BCryptPasswordHasher : IPasswordHasher
    {
        public string Hash(Password password) => BCrypt.Net.BCrypt.EnhancedHashPassword(password.Value, 13);

        public bool Verify(Password password, string hashedPassword) =>  BCrypt.Net.BCrypt.EnhancedVerify(password.Value, hashedPassword);
    }
}