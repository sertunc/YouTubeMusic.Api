using SelenyumMicroService.Api.Client.BaseClients;
using SelenyumMicroService.Shared.Dtos;
using System.Text.Json;
using System.Text;

namespace YouTubeMusic.Api.Business.Search
{
    public class SearchHttpClient : ISearchBusiness
    {
        private readonly HttpClient httpClient;
        private readonly ILogger<SearchHttpClient> logger;

        public SearchHttpClient(ILogger<SearchHttpClient> logger, HttpClient httpClient)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<Response<SearchResponseModel>> Search(SearchRequestModel searchRequestModel)
        {
            var content = new StringContent(JsonSerializer.Serialize(searchRequestModel), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("search?prettyPrint=false", content);

            var result = await ReadResponseStream(response);

            return null;
        }

        private static async Task<string> ReadResponseStream(HttpResponseMessage response)
        {
            using var streamReader = new StreamReader(await response.Content.ReadAsStreamAsync());
            var result = await streamReader.ReadToEndAsync();
            return result;
        }
    }
}