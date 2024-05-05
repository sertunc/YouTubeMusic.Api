using Newtonsoft.Json.Linq;
using System.Text;

namespace YouTubeMusic.Api.Business.Search
{
    public static class SearchParser
    {
        public static SearchResponseModel Parse(string json)
        {
            var jsonObject = JObject.Parse(json);
            var resultRootObject = GetRootObject(jsonObject);

            var title = resultRootObject?["title"]?["runs"]?[0]?["text"]?.ToString();

            var subtitleResult = new StringBuilder();
            var subtitleJToken = resultRootObject?["subtitle"]?["runs"]?.ToString();
            if (subtitleJToken != null)
            {
                foreach (JToken token in JArray.Parse(subtitleJToken))
                {
                    if (token["text"] != null)
                    {
                        subtitleResult.Append(token["text"]?.ToString());
                    }
                }
            }

            var thumbnailUrl = "";
            var thumbnailJToken = resultRootObject?["thumbnail"]?["musicThumbnailRenderer"]?["thumbnail"]?["thumbnails"]?.ToString();
            if (thumbnailJToken != null)
            {
                var thumbnailJArray = JArray.Parse(thumbnailJToken);
                thumbnailUrl = thumbnailJArray[thumbnailJArray.Count - 1]?["url"]?.ToString();
            }

            var watchId = resultRootObject?["onTap"]?["watchEndpoint"]?["videoId"]?.ToString();

            return new SearchResponseModel
            {
                Title = string.IsNullOrEmpty(title) ? "Not found" : title,
                Subtitle = subtitleResult.ToString(),
                WatchId = string.IsNullOrEmpty(watchId) ? "Not found" : watchId,
                ThumbnailUrl = string.IsNullOrEmpty(thumbnailUrl) ? "Not found" : thumbnailUrl,
            };
        }

        private static JToken? GetRootObject(JObject obj)
        {
            return obj?["contents"]?["tabbedSearchResultsRenderer"]?["tabs"]?[0]?["tabRenderer"]?["content"]?["sectionListRenderer"]?["contents"]?[0]?["musicCardShelfRenderer"];
        }
    }
}
