using System.Net.Http.Headers;

namespace YouTubeMusic.Api.BootStrappers
{
    public static class ApiClientBootStrapper
    {
        public static void AddCustomHttpClient<TClient, TImplementation>(this IServiceCollection services, string baseUrl, string mediaType, int timeoutAsSeconds = 120)
            where TClient : class
            where TImplementation : class, TClient
        {
            services.AddHttpClient<TClient, TImplementation>(client =>
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
                client.Timeout = TimeSpan.FromSeconds(timeoutAsSeconds);
            });
        }

        public static void AddCustomHttpClient<TClient, TImplementation>(this IServiceCollection services, string baseUrl, int timeoutAsSeconds = 120)
            where TClient : class
            where TImplementation : class, TClient
        {
            services.AddHttpClient<TClient, TImplementation>(client =>
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(timeoutAsSeconds);
            });
        }

        public static void AddCustomHttpClient<TImplementation>(this IServiceCollection services, string baseUrl, int timeoutAsSeconds = 120)
            where TImplementation : class
        {
            services.AddHttpClient<TImplementation>(client =>
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(timeoutAsSeconds);
            });
        }
    }
}