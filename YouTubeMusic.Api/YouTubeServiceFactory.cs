using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;

namespace YouTubeMusic.Api
{
    public class YouTubeServiceFactory
    {
        private readonly YouTubeService _youTubeService;

        public YouTubeServiceFactory(string clientSecretJsonPath)
        {
            UserCredential credential;
            using (var stream = new FileStream(clientSecretJsonPath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    [YouTubeService.Scope.Youtube],
                    "user",
                    CancellationToken.None,
                    new FileDataStore(this.GetType().ToString())).GetAwaiter().GetResult();
            }

            _youTubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = this.GetType().ToString()
            });
        }

        public YouTubeService GetYouTubeService() => _youTubeService;
    }
}