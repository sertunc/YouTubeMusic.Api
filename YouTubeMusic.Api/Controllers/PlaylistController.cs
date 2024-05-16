using Google.Apis.YouTube.v3.Data;
using Microsoft.AspNetCore.Mvc;
using YouTubeMusic.Api.Business.Playlist.Models;

namespace YouTubeMusic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly ILogger<SearchController> logger;
        private readonly YouTubeServiceFactory youTubeServiceFactory;

        public PlaylistController(ILogger<SearchController> logger, YouTubeServiceFactory youTubeServiceFactory)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.youTubeServiceFactory = youTubeServiceFactory ?? throw new ArgumentNullException(nameof(youTubeServiceFactory));
        }

        //C:\Users\sertu\AppData\Roaming\YouTubeMusic.Api.Controllers.PlaylistController
        //https://github.com/youtube/api-samples/tree/master/dotnet
        //https://console.cloud.google.com/apis/credentials/consent?project=spotfiy-to-youtube-music
        [HttpPost]
        public async Task<IActionResult> Add(PlaylistAddRequestModel playlistAddRequestModel)
        {
            logger.LogDebug("Adding playlist with name {PlaylistName}", playlistAddRequestModel.Title);

            var youtubeService = youTubeServiceFactory.GetYouTubeService();

            // Create a new, private playlist in the authorized user's channel.
            var newPlaylist = new Playlist
            {
                Snippet = new PlaylistSnippet
                {
                    Title = playlistAddRequestModel.Title,
                    Description = playlistAddRequestModel.Description
                },
                Status = new PlaylistStatus
                {
                    PrivacyStatus = playlistAddRequestModel.PrivacyStatus
                }
            };

            var result = await youtubeService.Playlists.Insert(newPlaylist, "snippet,status").ExecuteAsync();

            //return StatusCode(result.StatusCode, result);
            return Ok();
        }
    }
}