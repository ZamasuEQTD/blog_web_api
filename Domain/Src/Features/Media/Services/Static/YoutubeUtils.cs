using System.Text.RegularExpressions;

namespace Domain.Medias.Services
{
    public static class YoutubeUtils
    {
        private static readonly Regex YOUTUBE_ID_REGEX = new Regex(@"(youtu.*be.*)\/(watch\?v=|embed\/|v|shorts|)(.*?((?=[&#?])|$))");
        private static readonly Regex YOUTUBE_LINK_REGEX = new Regex(@"^https?:\/\/(www\.)?youtu(\.be\/|be\.com\/)(watch\?v=|embed\/|v\/|shorts\/)([A-Za-z0-9-_]{11})((?=[&#?])|$)");
        public static bool EsYoutubeVideo(string url) => YOUTUBE_LINK_REGEX.IsMatch(url);

        public static string GetVideoId(string url)
        {
            var match = YOUTUBE_ID_REGEX.Match(url);

            if (!match.Success) throw new Exception("Link invalido");

            return match.Groups[3].Value;
        }
        public static string GetVideoThumbnailFromId(string id) => $"https://img.youtube.com/vi/{id}/0.jpg";
        public static string GetVideoThumbnailFromUrl(string url) => GetVideoThumbnailFromId(GetVideoId(url));
    }

}