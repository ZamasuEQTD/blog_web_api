using System.Security.Cryptography;
using System.Text;
using Domain.Media.Abstractions;

namespace Infraestructure.Media {
    public class MediaHasher : IMediaHasher
    {
        public async Task<string> HashStream(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            byte[] hash;
            
            using (var md5 = MD5.Create()) {
                hash = await md5.ComputeHashAsync(stream);
            }

            return Convert.ToBase64String(hash);
        }

        public async Task<string> HashString(string url)
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(url)))
            {
                return await HashStream(stream);
            }
        }

    }
}