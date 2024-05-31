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

        public async Task<Response<List<PlaylistResponseModel>>> List(string userId)
        {
            try
            {
                ArgumentException.ThrowIfNullOrEmpty(userId);

                var youtubeService = youTubeServiceFactory.GetYouTubeService(userId);

                var listRequest = youtubeService.Playlists.List(ResourceProperties);
                listRequest.Mine = true;
                listRequest.MaxResults = int.MaxValue;

                var youtubeServiceResult = await listRequest.ExecuteAsync();

                if (youtubeServiceResult != null)
                {
                    var result = youtubeServiceResult.Items.
                        Select(x => new PlaylistResponseModel
                        {
                            ChannelId = string.IsNullOrEmpty(x.Snippet.ChannelId) ? "Not found" : x.Snippet.ChannelId,
                            ChannelTitle = string.IsNullOrEmpty(x.Snippet.ChannelTitle) ? "Not found" : x.Snippet.ChannelTitle,
                            Id = string.IsNullOrEmpty(x.Id) ? "Not found" : x.Id,
                            Name = string.IsNullOrEmpty(x.Snippet.Title) ? "Not found" : x.Snippet.Title,
                            Description = x.Snippet.Description,
                            ImageUrl = x.Snippet.Thumbnails.Default__.Url,
                        }).ToList();

                    return Response<List<PlaylistResponseModel>>.Success(result);
                }
                else
                {
                    return Response<List<PlaylistResponseModel>>.Fail("Playlist could not be listed!");
                }
            }
            catch (Exception ex)
            {
                return Response<List<PlaylistResponseModel>>.Fail(ex.Message);
            }
        }

        public async Task<Response<PlaylistCreateResponseModel>> Create(PlaylistCreateRequestModel model)
        {
            try
            {
                ArgumentException.ThrowIfNullOrEmpty(model.UserId);
                ArgumentException.ThrowIfNullOrEmpty(model.Title);

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

        public async Task<Response<bool>> Delete(PlaylistDeleteRequestModel model)
        {
            try
            {
                ArgumentException.ThrowIfNullOrEmpty(model.UserId);
                ArgumentException.ThrowIfNullOrEmpty(model.PlaylistId);

                var youtubeService = youTubeServiceFactory.GetYouTubeService(model.UserId);

                var youtubeServiceResult = await youtubeService.Playlists.Delete(model.PlaylistId).ExecuteAsync();

                if (youtubeServiceResult != null)
                {
                    return Response<bool>.Success(true);
                }
                else
                {
                    return Response<bool>.Fail("Playlist could not be deleted!");
                }
            }
            catch (Exception ex)
            {
                return Response<bool>.Fail(ex.Message);
            }
        }

        public async Task<Response<AddPlaylistItemResponseModel>> AddPlaylistItem(AddPlaylistItemRequestModel model)
        {
            try
            {
                ArgumentException.ThrowIfNullOrEmpty(model.UserId);
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

        public async Task<Response<bool>> DeletePlaylistItem(DeletePlaylistItemRequestModel model)
        {
            try
            {
                ArgumentException.ThrowIfNullOrEmpty(model.UserId);
                ArgumentException.ThrowIfNullOrEmpty(model.PlaylistItemId);

                var youtubeService = youTubeServiceFactory.GetYouTubeService(model.UserId);

                //which playlist?
                //bug here
                var youtubeServiceResult = await youtubeService.PlaylistItems.Delete(model.PlaylistItemId).ExecuteAsync();

                if (youtubeServiceResult != null)
                {
                    return Response<bool>.Success(true);
                }
                else
                {
                    return Response<bool>.Fail("Playlist item could not be deleted!");
                }
            }
            catch (Exception ex)
            {
                return Response<bool>.Fail(ex.Message);
            }
        }
    }
}