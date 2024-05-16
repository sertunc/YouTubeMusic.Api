using Microsoft.AspNetCore.Mvc;
using YouTubeMusic.Api.Business.Search;
using YouTubeMusic.Api.Business.Search.Models;

namespace YouTubeMusic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> logger;
        private readonly ISearchBusiness searchBusiness;

        public SearchController(ILogger<SearchController> logger, ISearchBusiness searchBusiness)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.searchBusiness = searchBusiness ?? throw new ArgumentNullException(nameof(searchBusiness));
        }

        [HttpPost]
        public async Task<IActionResult> Search(SearchRequestModel searchRequestModel)
        {
            logger.LogDebug("Getting search by data={@Data}", searchRequestModel);

            var result = await searchBusiness.Search(searchRequestModel);

            return StatusCode(result.StatusCode, result);
        }
    }
}