using SelenyumMicroService.Shared.Dtos;
using YouTubeMusic.Api.Business.Search.Models;

namespace YouTubeMusic.Api.Business.Search
{
    public interface ISearchBusiness
    {
        Task<Response<List<SearchResponseModel>>> Search(SearchRequestModel searchRequestModel);
    }
}