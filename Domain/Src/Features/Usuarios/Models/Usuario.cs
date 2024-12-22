using Microsoft.AspNetCore.Identity;

namespace Domain.Usuarios
{
    public class Role : IdentityRole<UsuarioId>, IEquatable<Role>
    {
        public string ShortName { get; set; }

        public override int GetHashCode()
        {
            return Name!.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            return obj is Role && Equals(obj);
        }

        public bool Equals(Role? other)
        {
            return other != null && other.Name == Name;
        }

        public static Role Anonimo = new Role { 
            Id = new UsuarioId(Guid.NewGuid()), 
            Name = "Anonimo", 
            ShortName = "Anon",
            NormalizedName = "ANONIMO"
        };
        public static Role Moderador = new Role { 
            Id = new UsuarioId(Guid.NewGuid()), 
            Name = "Moderador", 
            ShortName = "Mod",
            NormalizedName = "MODERADOR"
        };

        public static Role Owner = new Role { 
            Id = new UsuarioId(Guid.NewGuid()), 
            Name = "Owner", 
            ShortName = "Owner",
            NormalizedName = "Owner"
        };

        public static List<Role> Roles = new List<Role> { Anonimo, Moderador, Owner };
    }

    public class Usuario : IdentityUser<UsuarioId>
    {
        public string? Moderador {get;set;}
    }

    public class Autor
    {
        public string Nombre { get; set; }
        public string Rango { get; set; }	

        private Autor() {}
        public Autor(string nombre, string rango)
        {
            this.Nombre = nombre;
            this.Rango = rango;
        }
    }
}