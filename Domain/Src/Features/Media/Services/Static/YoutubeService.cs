using System.Text.RegularExpressions;

namespace Domain.Media.Services
{
    public static class YoutubeService
    {
        private static readonly Regex YOUTUBE_LINK_DETECTOR = new Regex(@"^(https?://)?(www\.)?(youtube\.com|youtu\.?be)/.+$");
        private static readonly Regex YOUTUBE_VIDEO_SHORT_DETECTOR = new Regex(@"^(https?://)?(www\.)?(youtube\.com|youtu\.?be)/.+$");
        public static string GetVideoThumbnailFromId(string videoId) => $"https://img.youtube.com/vi/{videoId}/hqdefault.jpg";
        public static string GetVideoThumbnailFromUrl(string url) => GetVideoThumbnailFromId(GetVideoId(url));
        public static string GetVideoId(string url) => $"";
        public static bool IsShortOrVideo(string url) => YOUTUBE_VIDEO_SHORT_DETECTOR.IsMatch(url);
        public static bool IsYoutubeLink(string url) => YOUTUBE_LINK_DETECTOR.IsMatch(url);
    }
}