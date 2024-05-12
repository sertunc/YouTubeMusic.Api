using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YouTubeMusic.Api.Business.Search;

namespace YouTubeMusic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly ILogger<SearchController> logger;

        public PlaylistController(ILogger<SearchController> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        //C:\Users\sertu\AppData\Roaming\YouTubeMusic.Api.Controllers.PlaylistController
        //https://github.com/youtube/api-samples/tree/master/dotnet
        //https://console.cloud.google.com/apis/credentials/consent?project=spotfiy-to-youtube-music
        [HttpPost]
        public async Task<IActionResult> Add()
        {
            //logger.LogDebug("Getting search by data={@Data}", searchRequestModel);

            UserCredential credential;
            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    [YouTubeService.Scope.Youtube],
                    "user",
                    CancellationToken.None,
                    new FileDataStore(this.GetType().ToString())
                );
            }

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = this.GetType().ToString()
            });

            // Create a new, private playlist in the authorized user's channel.
            var newPlaylist = new Playlist();
            newPlaylist.Snippet = new PlaylistSnippet();
            newPlaylist.Snippet.Title = "Test Playlist";
            newPlaylist.Snippet.Description = "A playlist created with the YouTube API v3";
            newPlaylist.Status = new PlaylistStatus();
            newPlaylist.Status.PrivacyStatus = "public";
            var result = await youtubeService.Playlists.Insert(newPlaylist, "snippet,status").ExecuteAsync();

            //return StatusCode(result.StatusCode, result);
            return Ok();
        }
    }
}
