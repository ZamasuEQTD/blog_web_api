using System.Security.Cryptography;
using System.Text;

namespace Application.Common.Services
{
    static public class Hasher
    {
        private static readonly HashAlgorithm _encripter = MD5.Create();
        static public async Task<string> Hash(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            byte[] hash;

            hash = await _encripter.ComputeHashAsync(stream);

            return string
                .Join("", hash.Select(e => e.ToString("x2")));
        }

        static public async Task<string> Hash(string url)
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(url)))
            {
                return await Hash(stream);
            }
        }
    }
}