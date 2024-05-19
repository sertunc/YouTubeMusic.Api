using SelenyumMicroService.Shared.Dtos;
using YouTubeMusic.Api.Business.Login.Models;

namespace YouTubeMusic.Api.Business.Login
{
    public class LoginBusiness : ILoginBusiness
    {
        private const string ResourceProperties = "snippet,contentDetails,statistics";

        private readonly YouTubeServiceFactory youTubeServiceFactory;

        public LoginBusiness(YouTubeServiceFactory youTubeServiceFactory)
        {
            this.youTubeServiceFactory = youTubeServiceFactory ?? throw new ArgumentNullException(nameof(youTubeServiceFactory));
        }

        public async Task<Response<LoginResponseModel>> Login(LoginRequestModel model)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(model.UserId);

                var youtubeService = youTubeServiceFactory.GetYouTubeService(model.UserId);

                var channelsRequest = youtubeService.Channels.List(ResourceProperties);
                channelsRequest.Mine = true;

                var channelsResponse = await channelsRequest.ExecuteAsync();

                if (channelsResponse == null || channelsResponse.Items.Count == 0)
                {
                    return Response<LoginResponseModel>.Fail("User not found!");
                }

                var result = new LoginResponseModel
                {
                    Title = string.IsNullOrEmpty(channelsResponse.Items[0].Snippet.Title) ? "Not found" : channelsResponse.Items[0].Snippet.Title,
                    Description = string.IsNullOrEmpty(channelsResponse.Items[0].Snippet.Description) ? "Not found" : channelsResponse.Items[0].Snippet.Description,
                    CustomUrl = string.IsNullOrEmpty(channelsResponse.Items[0].Snippet.CustomUrl) ? "Not found" : channelsResponse.Items[0].Snippet.CustomUrl,
                };

                return Response<LoginResponseModel>.Success(result);
            }
            catch (Exception ex)
            {
                return Response<LoginResponseModel>.Fail(ex.Message);
            }
        }
    }
}
