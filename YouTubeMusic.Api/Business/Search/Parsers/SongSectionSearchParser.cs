using Newtonsoft.Json.Linq;
using System.Text;
using YouTubeMusic.Api.Business.Search.Models;

namespace YouTubeMusic.Api.Business.Search.Parsers
{
    public class SongSectionSearchParser : BaseSearchParser, ISearchParser
    {
        public List<SearchResponseModel> Parse(string json)
        {
            var jsonObject = JObject.Parse(json);
            var songSection = base.GetRootObject(jsonObject)?[1]?["musicShelfRenderer"]?["contents"]?.ToString();

            var searchResponseModelList = new List<SearchResponseModel>();

            if (songSection != null)
            {
                foreach (JToken song in JArray.Parse(songSection))
                {
                    var flexColumnFirstValue = true;

                    var flexColumns = song["musicResponsiveListItemRenderer"]?["flexColumns"]?.ToString();

                    var title = "";
                    var subtitleResult = new StringBuilder();

                    if (flexColumns != null)
                    {
                        foreach (JToken flexColumn in JArray.Parse(flexColumns))
                        {
                            var titleList = flexColumn["musicResponsiveListItemFlexColumnRenderer"]?["text"]?["runs"]?.ToString();
                            if (titleList != null)
                            {
                                foreach (JToken titleItem in JArray.Parse(titleList))
                                {
                                    if (flexColumnFirstValue)
                                    {
                                        title = titleItem?["text"]?.ToString();
                                        flexColumnFirstValue = false;
                                    }
                                    else
                                    {
                                        subtitleResult.Append(titleItem?["text"]?.ToString());
                                    }
                                }
                            }
                        }
                    }

                    var thumbnailUrl = "";
                    var thumbnail = song["musicResponsiveListItemRenderer"]?["thumbnail"]?["musicThumbnailRenderer"]?["thumbnail"]?["thumbnails"]?.ToString();
                    if (thumbnail != null)
                    {
                        var thumbnailArray = JArray.Parse(thumbnail);
                        thumbnailUrl = thumbnailArray[thumbnailArray.Count - 1]?["url"]?.ToString();
                    }

                    var watchId = song?["musicResponsiveListItemRenderer"]?["playlistItemData"]?["videoId"]?.ToString();

                    searchResponseModelList.Add(new SearchResponseModel
                    {
                        Title = string.IsNullOrEmpty(title) ? "Not found" : title,
                        Subtitle = subtitleResult.ToString(),
                        WatchId = string.IsNullOrEmpty(watchId) ? "Not found" : watchId,
                        ThumbnailUrl = string.IsNullOrEmpty(thumbnailUrl) ? "Not found" : thumbnailUrl,
                    });
                }
            }

            return searchResponseModelList;
        }
    }
}