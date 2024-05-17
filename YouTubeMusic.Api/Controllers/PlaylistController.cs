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
        public async Task<IActionResult> Add(PlaylistCreateRequestModel model)
        {
            logger.LogDebug("Adding playlist with name {Title}", model.Title);

            var result = await playlistBusiness.Create(model);

            return StatusCode(result.StatusCode, result);
        }
    }
}