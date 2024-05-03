﻿using SelenyumMicroService.Shared.Dtos;

namespace YouTubeMusic.Api.Business.Search
{
    public class SearchBusiness : ISearchBusiness
    {
        private readonly SearchHttpClient searchHttpClient;

        public SearchBusiness(SearchHttpClient searchHttpClient)
        {
            this.searchHttpClient = searchHttpClient ?? throw new ArgumentNullException(nameof(searchHttpClient));
        }

        public async Task<Response<SearchResponseModel>> Search(SearchRequestModel searchRequestModel)
        {
            var result = await searchHttpClient.Search(searchRequestModel);

            //return Task.FromResult(Response<SearchResponseModel>.Success(new SearchResponseModel()
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    Name = searchRequestModel.Query
            //}, 200));

            return null;
        }
    }
}