using System.Text;
using Domain.Comentarios.ValueObjects;
using Domain.Hilos;
using Domain.Usuarios;

namespace Domain.Comentarios.Services
{
    static public class RandomTextBuilderService
    {
        private static readonly string _caracteres_string = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public static readonly List<char> caracteres = _caracteres_string.ToList();
        static public string BuildRandomString(Random random, int length)
        {
            StringBuilder builder = new();

            for (int i = length - 1; i >= 0; i--)
            {
                int randomIndex = random.Next(caracteres.Count);

                builder.Append(caracteres[randomIndex]);
            }
            return builder.ToString();
        }
    }
    static public class TagsService
    {
        static private readonly Random _random = new Random();
        static public Tag GenerarTag() => Tag.Create(RandomTextBuilderService.BuildRandomString(_random, Tag.LENGTH)).Value;
        static public TagUnico GenerarTagUnico(HiloId hiloId, UsuarioId usuarioId) => TagUnico.Create(RandomTextBuilderService.BuildRandomString(
            new Random((hiloId.ToString() + usuarioId.ToString()).GetHashCode()),
            TagUnico.Lenght
        )).Value;
    }

    static public class DadosService
    {
        static private readonly Random _random = new Random();
        static public Dados Generar() => Dados.Create(_random.Next(Dados.MIN, Dados.MAX)).Value;
    }
}