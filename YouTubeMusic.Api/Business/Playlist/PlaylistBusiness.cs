using Google.Apis.YouTube.v3.Data;
using SelenyumMicroService.Shared.Dtos;
using YouTubeMusic.Api.Business.Playlist.Models;

namespace YouTubeMusic.Api.Business.Playlist
{
    public class PlaylistBusiness : IPlaylistBusiness
    {
        private readonly YouTubeServiceFactory youTubeServiceFactory;

        public PlaylistBusiness(YouTubeServiceFactory youTubeServiceFactory)
        {
            this.youTubeServiceFactory = youTubeServiceFactory ?? throw new ArgumentNullException(nameof(youTubeServiceFactory));
        }

        public async Task<Response<PlaylistCreateResponseModel>> Create(PlaylistCreateRequestModel model)
        {
            try
            {
                var youtubeService = youTubeServiceFactory.GetYouTubeService();

                var newPlaylist = new Google.Apis.YouTube.v3.Data.Playlist
                {
                    Snippet = new PlaylistSnippet
                    {
                        Title = model.Title,
                        Description = model.Description
                    },
                    Status = new PlaylistStatus
                    {
                        PrivacyStatus = model.PrivacyStatus
                    }
                };

                var youtubeServiceResult = await youtubeService.Playlists.Insert(newPlaylist, "snippet,status").ExecuteAsync();

                if (youtubeServiceResult != null)
                {
                    var result = new PlaylistCreateResponseModel
                    {
                        Id = string.IsNullOrEmpty(youtubeServiceResult.Id) ? "Not found" : youtubeServiceResult.Id,
                        ChannelId = string.IsNullOrEmpty(youtubeServiceResult.Snippet.ChannelId) ? "Not found" : youtubeServiceResult.Snippet.ChannelId,
                        ChannelTitle = string.IsNullOrEmpty(youtubeServiceResult.Snippet.ChannelTitle) ? "Not found" : youtubeServiceResult.Snippet.ChannelTitle,
                    };

                    return Response<PlaylistCreateResponseModel>.Success(result);
                }
                else
                {
                    return Response<PlaylistCreateResponseModel>.Fail("Playlist could not be created!");
                }
            }
            catch (Exception ex)
            {
                return Response<PlaylistCreateResponseModel>.Fail(ex.Message);
            }
        }
    }
}