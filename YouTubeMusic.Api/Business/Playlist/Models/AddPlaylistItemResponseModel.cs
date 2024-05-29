namespace YouTubeMusic.Api.Business.Playlist.Models
{
    public class AddPlaylistItemResponseModel
    {
        public string ChannelId { get; set; } = string.Empty;
        public string ChannelTitle { get; set; } = string.Empty;
        public string PlaylistId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string VideoOwnerChannelTitle { get; set; } = string.Empty;
        public string VideoOwnerChannelId { get; set; } = string.Empty;
    }
}