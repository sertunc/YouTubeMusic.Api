using SelenyumMicroService.Shared.Dtos;
using YouTubeMusic.Api.Business.Login.Models;

namespace YouTubeMusic.Api.Business.Login
{
    public interface ILoginBusiness
    {
        Task<Response<LoginResponseModel>> Login(LoginRequestModel model);
    }
}
