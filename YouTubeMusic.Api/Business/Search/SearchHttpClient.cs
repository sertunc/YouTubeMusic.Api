using SelenyumMicroService.Shared.Dtos;

namespace YouTubeMusic.Api.Business.Search
{
    public class SearchHttpClient
    {
        private readonly HttpClient httpClient;
        private readonly ILogger<SearchHttpClient> logger;

        public SearchHttpClient(ILogger<SearchHttpClient> logger, HttpClient httpClient)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<Response<string>> Search(SearchRequestModel searchRequestModel)
        {
            var httpClientResponse = await httpClient.PostAsJsonAsync("search?prettyPrint=false", searchRequestModel);

            httpClientResponse.EnsureSuccessStatusCode();

            var httpClientResponseContent = await httpClientResponse.Content.ReadAsStringAsync();

            return Response<string>.Success(httpClientResponseContent, (int)httpClientResponse.StatusCode);
        }
    }
}