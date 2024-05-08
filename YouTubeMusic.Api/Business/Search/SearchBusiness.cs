using SelenyumMicroService.Shared.Dtos;
using System.Net;
using YouTubeMusic.Api.Business.Search.Parsers;

namespace YouTubeMusic.Api.Business.Search
{
    public class SearchBusiness : ISearchBusiness
    {
        private readonly ILogger<SearchHttpClient> logger;
        private readonly IEnumerable<ISearchParser> searchParsers;
        private readonly SearchHttpClient searchHttpClient;

        public SearchBusiness(
            ILogger<SearchHttpClient> logger,
            IEnumerable<ISearchParser> searchParsers,
            SearchHttpClient searchHttpClient)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.searchParsers = searchParsers ?? throw new ArgumentNullException(nameof(searchParsers));
            this.searchHttpClient = searchHttpClient ?? throw new ArgumentNullException(nameof(searchHttpClient));
        }

        public async Task<Response<List<SearchResponseModel>>> Search(SearchRequestModel searchRequestModel)
        {
            try
            {
                var searchHttpClientResponse = await searchHttpClient.Search(searchRequestModel);

                var searchResponseModelList = new List<SearchResponseModel>();

                foreach (var item in searchParsers)
                {
                    var result = item.Parse(searchHttpClientResponse.Data);
                    searchResponseModelList.AddRange(result);
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