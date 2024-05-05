using SelenyumMicroService.Shared.Dtos;
using System.Net;

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
            try
            {
                var response = await httpClient.PostAsJsonAsync("search?prettyPrint=false", searchRequestModel);

                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();

                var searchResponseModel = SearchParser.Parse(result);

                return Response<SearchResponseModel>.Success(searchResponseModel);
            }
            catch (HttpRequestException httpRequestException)
            {
                return Response<SearchResponseModel>.Fail(httpRequestException.Message, (int)(httpRequestException.StatusCode ?? HttpStatusCode.InternalServerError));
            }
            catch (Exception ex)
            {
                return Response<SearchResponseModel>.Fail(ex.Message);
            }
        }
    }
}