using SelenyumMicroService.Shared.Dtos;

namespace YouTubeMusic.Api.Business.Search
{
    public interface ISearchBusiness
    {
        Task<Response<SearchResponseModel>> Search(SearchRequestModel searchRequestModel);
    }
}