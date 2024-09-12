using System.Security.Cryptography;
using System.Text;
using Domain.Media.Abstractions;

namespace Infraestructure.Media
{
    public class HasherCalculator : IHasherCalculator
    {
        public async Task<string> Hash(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            byte[] hash;

            using (var md5 = MD5.Create())
            {
                hash = await md5.ComputeHashAsync(stream);
            }

            return string.Join("", hash.Select(e => e.ToString("x2")));
        }

        public async Task<string> Hash(string url)
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(url)))
            {
                return await Hash(stream);
            }
        }
    }
}