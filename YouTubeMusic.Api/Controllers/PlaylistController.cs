using Microsoft.AspNetCore.Mvc;
using YouTubeMusic.Api.Business.Playlist;
using YouTubeMusic.Api.Business.Playlist.Models;

namespace YouTubeMusic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly ILogger<SearchController> logger;
        private readonly IPlaylistBusiness playlistBusiness;

        public PlaylistController(ILogger<SearchController> logger, IPlaylistBusiness playlistBusiness)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.playlistBusiness = playlistBusiness ?? throw new ArgumentNullException(nameof(playlistBusiness));
        }

        //C:\Users\sertu\AppData\Roaming\YouTubeMusic.Api.Controllers.PlaylistController
        //https://github.com/youtube/api-samples/tree/master/dotnet
        //https://console.cloud.google.com/apis/credentials/consent?project=spotfiy-to-youtube-music
        [HttpPost]
        public async Task<IActionResult> Create(PlaylistCreateRequestModel model)
        {
            logger.LogDebug("Creating playlist with name {Title}", model.Title);

            var result = await playlistBusiness.Create(model);

            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(PlaylistDeleteRequestModel model)
        {
            logger.LogDebug("Deleting playlist with name {Title}", model.PlaylistId);

            var result = await playlistBusiness.Delete(model);

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("addItem")]
        public async Task<IActionResult> AddPlaylistItem(AddPlaylistItemRequestModel model)
        {
            logger.LogDebug("Adding playlist item with PlaylistId {PlaylistId} WatchId {WatchId}", model.PlaylistId, model.WatchId);

            var result = await playlistBusiness.AddPlaylistItem(model);

            return StatusCode(result.StatusCode, result);
        }
    }
}