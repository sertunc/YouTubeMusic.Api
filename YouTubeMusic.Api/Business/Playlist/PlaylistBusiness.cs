using Google.Apis.YouTube.v3.Data;
using SelenyumMicroService.Shared.Dtos;
using YouTubeMusic.Api.Business.Playlist.Models;

namespace YouTubeMusic.Api.Business.Playlist
{
    public class PlaylistBusiness : IPlaylistBusiness
    {
        private const string ResourceProperties = "snippet,status";

        private readonly YouTubeServiceFactory youTubeServiceFactory;

        public PlaylistBusiness(YouTubeServiceFactory youTubeServiceFactory)
        {
            this.youTubeServiceFactory = youTubeServiceFactory ?? throw new ArgumentNullException(nameof(youTubeServiceFactory));
        }

        public async Task<Response<PlaylistCreateResponseModel>> Create(PlaylistCreateRequestModel model)
        {
            try
            {
                ArgumentException.ThrowIfNullOrEmpty(model.UserId);

                var youtubeService = youTubeServiceFactory.GetYouTubeService(model.UserId);

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

                var youtubeServiceResult = await youtubeService.Playlists.Insert(newPlaylist, ResourceProperties).ExecuteAsync();

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

        public async Task<Response<AddPlaylistItemResponseModel>> AddPlaylistItem(AddPlaylistItemRequestModel model)
        {
            try
            {
                ArgumentException.ThrowIfNullOrEmpty(model.PlaylistId);
                ArgumentException.ThrowIfNullOrEmpty(model.WatchId);

                var youtubeService = youTubeServiceFactory.GetYouTubeService(model.UserId);

                var newPlaylistItem = new PlaylistItem();
                newPlaylistItem.Snippet = new PlaylistItemSnippet();
                newPlaylistItem.Snippet.PlaylistId = model.PlaylistId;
                newPlaylistItem.Snippet.ResourceId = new ResourceId();
                newPlaylistItem.Snippet.ResourceId.Kind = "youtube#video";
                newPlaylistItem.Snippet.ResourceId.VideoId = model.WatchId;

                var youtubeServiceResult = await youtubeService.PlaylistItems.Insert(newPlaylistItem, ResourceProperties).ExecuteAsync();

                if (youtubeServiceResult != null)
                {
                    var result = new AddPlaylistItemResponseModel
                    {
                        ChannelId = string.IsNullOrEmpty(youtubeServiceResult.Snippet.ChannelId) ? "Not found" : youtubeServiceResult.Snippet.ChannelId,
                        ChannelTitle = string.IsNullOrEmpty(youtubeServiceResult.Snippet.ChannelTitle) ? "Not found" : youtubeServiceResult.Snippet.ChannelTitle,
                        PlaylistId = string.IsNullOrEmpty(youtubeServiceResult.Snippet.PlaylistId) ? "Not found" : youtubeServiceResult.Snippet.PlaylistId,
                        Title = string.IsNullOrEmpty(youtubeServiceResult.Snippet.Title) ? "Not found" : youtubeServiceResult.Snippet.Title,
                        VideoOwnerChannelTitle = string.IsNullOrEmpty(youtubeServiceResult.Snippet.VideoOwnerChannelTitle) ? "Not found" : youtubeServiceResult.Snippet.VideoOwnerChannelTitle,
                        VideoOwnerChannelId = string.IsNullOrEmpty(youtubeServiceResult.Snippet.VideoOwnerChannelId) ? "Not found" : youtubeServiceResult.Snippet.VideoOwnerChannelId,
                    };

                    return Response<AddPlaylistItemResponseModel>.Success(result);
                }
                else
                {
                    return Response<AddPlaylistItemResponseModel>.Fail("Playlist item could not be added!");
                }
            }
            catch (Exception ex)
            {
                return Response<AddPlaylistItemResponseModel>.Fail(ex.Message);
            }
        }
    }
}