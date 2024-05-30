namespace YouTubeMusic.Api.Business.Playlist.Models
{
    public record PlaylistDeleteRequestModel(string UserId, string PlaylistId) : BaseRequestModel(UserId);
}