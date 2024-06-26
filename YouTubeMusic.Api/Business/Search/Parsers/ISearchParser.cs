﻿using YouTubeMusic.Api.Business.Search.Models;

namespace YouTubeMusic.Api.Business.Search.Parsers
{
    public interface ISearchParser
    {
        List<SearchResponseModel> Parse(string json);
    }
}