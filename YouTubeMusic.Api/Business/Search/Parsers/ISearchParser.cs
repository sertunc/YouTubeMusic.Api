namespace YouTubeMusic.Api.Business.Search.Parsers
{
    public interface ISearchParser
    {
        SearchResponseModel Parse(string json);
    }
}