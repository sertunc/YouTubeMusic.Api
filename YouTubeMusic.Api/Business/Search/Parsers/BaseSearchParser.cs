using Newtonsoft.Json.Linq;

namespace YouTubeMusic.Api.Business.Search.Parsers
{
    public abstract class BaseSearchParser
    {
        public virtual JToken? GetRootObject(JObject obj)
        {
            return obj?["contents"]?["tabbedSearchResultsRenderer"]?["tabs"]?[0]?["tabRenderer"]?["content"]?["sectionListRenderer"]?["contents"];
        }
    }
}