namespace YouTubeMusic.Api.Business.Playlist.Models
{
    public record PlaylistCreateRequestModel(string UserId, string Title, string Description, string PrivacyStatus) : BaseRequestModel(UserId);
}