using SelenyumMicroService.Shared.Dtos;

namespace YouTubeMusic.Api.Business.Search
{
    public interface ISearchBusiness
    {
        Task<Response<List<SearchResponseModel>>> Search(SearchRequestModel searchRequestModel);
    }
}