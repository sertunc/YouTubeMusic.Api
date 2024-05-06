using SelenyumMicroService.Shared.Dtos;
using System.Net;
using YouTubeMusic.Api.Business.Search.Parsers;

namespace YouTubeMusic.Api.Business.Search
{
    public class SearchHttpClient : ISearchBusiness
    {
        private readonly HttpClient httpClient;
        private readonly IEnumerable<ISearchParser> searchParsers;
        private readonly ILogger<SearchHttpClient> logger;

        public SearchHttpClient(ILogger<SearchHttpClient> logger, IEnumerable<ISearchParser> searchParsers, HttpClient httpClient)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.searchParsers = searchParsers ?? throw new ArgumentNullException(nameof(searchParsers));
        }

        public async Task<Response<List<SearchResponseModel>>> Search(SearchRequestModel searchRequestModel)
        {
            try
            {
                var httpClientResponse = await httpClient.PostAsJsonAsync("search?prettyPrint=false", searchRequestModel);

                httpClientResponse.EnsureSuccessStatusCode();

                var httpClientResponseContent = await httpClientResponse.Content.ReadAsStringAsync();

                var searchResponseModelList = new List<SearchResponseModel>();

                foreach (var item in searchParsers)
                {
                    var result = item.Parse(httpClientResponseContent);
                    searchResponseModelList.Add(result);
                }

                return Response<List<SearchResponseModel>>.Success(searchResponseModelList);
            }
            catch (HttpRequestException httpRequestException)
            {
                return Response<List<SearchResponseModel>>.Fail(httpRequestException.Message, (int)(httpRequestException.StatusCode ?? HttpStatusCode.InternalServerError));
            }
            catch (Exception ex)
            {
                return Response<List<SearchResponseModel>>.Fail(ex.Message);
            }
        }
    }
}