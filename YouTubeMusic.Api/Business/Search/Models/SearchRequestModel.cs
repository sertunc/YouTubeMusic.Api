namespace YouTubeMusic.Api.Business.Search.Models
{
    public class SearchRequestModel
    {
        public Context Context { get; set; } = new();
        public string Query { get; set; } = string.Empty;
    }

    public class Context
    {
        public Client Client { get; set; } = new();
    }

    public class Client
    {
        //TODO: these values can be retrieved from the appsettings if they change occasionally
        public string gl { get; set; } = "TR";
        public string hl { get; set; } = "tr";
        public string VisitorData { get; set; } = "CgtDMjlaN2doMVVkayj38tSxBjIKCgJUUhIEGgAgGw%3D%3D";

        public string ClientName { get; set; } = "WEB_REMIX";

        public string ClientVersion { get; set; } = "1.20240501.01.00";
    }
}