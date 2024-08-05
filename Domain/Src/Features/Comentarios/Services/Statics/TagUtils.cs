
using System.Text.RegularExpressions;
using Domain.Comentarios.ValueObjects;

namespace Domain.Comentarios.Services
{
    public static  class TagUtils {
        static private readonly string REGEX_STRING = ">>" + Tag.REGEX_STRING;
        
        static public List<Tag> GetTags(string texto){
            List<Tag> tags = [];

            var matches =GetMatches(texto);

            foreach (Match match in matches) {
                tags.Add(Tag.Create(match.Value.Substring(2)));
            }

            return tags;
        }

        static public HashSet<Tag> GetTagsUnicos(string texto){
            HashSet<Tag> tags = new HashSet<Tag>();
            
            var matches = GetMatches(texto);
            
            foreach (Match match in matches) {
                tags.Add(Tag.Create(match.Value.Substring(2)));
            }

            return tags;            
        }

        static private MatchCollection GetMatches(string texto) => Regex.Matches(texto, REGEX_STRING);
        static public int CantidadDeTags(string texto)=> GetTags(texto).Count;
        static public int CantidadDeTagsUnicos(string texto)=> GetTagsUnicos(texto).Count;
    }
}