namespace YouTubeMusic.Api.Business.Playlist.Models
{
    public record DeletePlaylistItemRequestModel(string UserId, string PlaylistItemId) : BaseRequestModel(UserId);
}