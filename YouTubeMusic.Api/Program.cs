using System.Net.Http.Headers;
using YouTubeMusic.Api;
using YouTubeMusic.Api.Business.Login;
using YouTubeMusic.Api.Business.Playlist;
using YouTubeMusic.Api.Business.Search;
using YouTubeMusic.Api.Business.Search.Parsers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var baseUrl = builder.Configuration["YouTubeMusicApiUrls:Base"];
ArgumentNullException.ThrowIfNull(baseUrl);

builder.Services.AddScoped<YouTubeServiceFactory>();

builder.Services.AddScoped<ILoginBusiness, LoginBusiness>();

builder.Services.AddHttpClient<SearchHttpClient>(client =>
{
    client.BaseAddress = new Uri(baseUrl);
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});
builder.Services.AddScoped<ISearchParser, BestResultSearchParser>();
builder.Services.AddScoped<ISearchParser, SongSectionSearchParser>();
builder.Services.AddScoped<ISearchBusiness, SearchBusiness>();

builder.Services.AddScoped<IPlaylistBusiness, PlaylistBusiness>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();