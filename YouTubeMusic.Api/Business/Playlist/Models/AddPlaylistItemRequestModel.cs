namespace YouTubeMusic.Api.Business.Playlist.Models
{
    public record AddPlaylistItemRequestModel(string UserId, string PlaylistId, string WatchId) : BaseRequestModel(UserId);
}