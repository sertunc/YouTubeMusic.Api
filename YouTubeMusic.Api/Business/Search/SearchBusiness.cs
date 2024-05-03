using SelenyumMicroService.Shared.Dtos;

namespace YouTubeMusic.Api.Business.Search
{
    public class SearchBusiness : ISearchBusiness
    {
        public Task<Response<SearchResponseModel>> Search(SearchRequestModel searchRequestModel)
        {
            return Task.FromResult(Response<SearchResponseModel>.Success(new SearchResponseModel()
            {
                Id = Guid.NewGuid().ToString(),
                Name = searchRequestModel.Query
            }, 200));
        }
    }
}