using Domain.Media.Abstractions;

namespace Infraestructure.Media
{
    public class FfmpegVideoVistaPreviaService : IVistaPreviaService
    {
        static private readonly NReco.VideoConverter.FFMpegConverter FFmpeg = new NReco.VideoConverter.FFMpegConverter();
        public Stream GenerarDesdeVideo(string path)
        {
            Stream stream = new MemoryStream();
            FFmpeg.GetVideoThumbnail(path, stream);

            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
    }
}