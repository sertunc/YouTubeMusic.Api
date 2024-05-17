using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;

namespace YouTubeMusic.Api
{
    public class YouTubeServiceFactory
    {
        public YouTubeService GetYouTubeService(string userId)
        {
            UserCredential credential;
            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                var dataStore = new FileDataStore($"{this.GetType()}_{userId}");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    [YouTubeService.Scope.Youtube],
                    "user",
                    CancellationToken.None,
                    dataStore).GetAwaiter().GetResult();
            }

            return new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = this.GetType().ToString()
            });
        }
    }
}