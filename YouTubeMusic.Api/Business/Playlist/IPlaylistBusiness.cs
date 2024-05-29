using SelenyumMicroService.Shared.Dtos;
using YouTubeMusic.Api.Business.Playlist.Models;

namespace YouTubeMusic.Api.Business.Playlist
{
    public interface IPlaylistBusiness
    {
        Task<Response<PlaylistCreateResponseModel>> Create(PlaylistCreateRequestModel model);

        Task<Response<AddPlaylistItemResponseModel>> AddPlaylistItem(AddPlaylistItemRequestModel model);
    }
}