using SelenyumMicroService.Shared.Dtos;
using YouTubeMusic.Api.Business.Playlist.Models;

namespace YouTubeMusic.Api.Business.Playlist
{
    public interface IPlaylistBusiness
    {
        Task<Response<List<PlaylistResponseModel>>> List(string userId);

        Task<Response<PlaylistCreateResponseModel>> Create(PlaylistCreateRequestModel model);

        Task<Response<bool>> Delete(PlaylistDeleteRequestModel model);

        Task<Response<AddPlaylistItemResponseModel>> AddPlaylistItem(AddPlaylistItemRequestModel model);

        Task<Response<bool>> DeletePlaylistItem(DeletePlaylistItemRequestModel model);
    }
}