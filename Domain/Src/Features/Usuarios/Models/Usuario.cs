using Microsoft.AspNetCore.Identity;

namespace Domain.Usuarios
{
    public class Role : IdentityRole<UsuarioId>
    {
        public string ShortName { get; set; }

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

        public static IEnumerable<Role> Roles = new List<Role> { Anonimo, Moderador, Owner };
    }

    public class Usuario : IdentityUser<UsuarioId>
    {
        public string? Moderador {get;set;}
        public DateTime RegistradoEn {get;set;}
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