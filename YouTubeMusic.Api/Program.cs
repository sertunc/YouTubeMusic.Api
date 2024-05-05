using System.Net.Http.Headers;
using YouTubeMusic.Api.Business.Search;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var baseUrl = builder.Configuration["YouTubeMusicApiUrls:Base"];
ArgumentNullException.ThrowIfNull(baseUrl);

//var searchUrl = builder.Configuration["YouTubeMusicApiUrls:Search"];
//ArgumentNullException.ThrowIfNull(searchUrl);
//builder.Services.AddCustomHttpClient<ISearchBusiness, SearchHttpClient>(string.Concat(baseUrl, searchUrl));
builder.Services.AddHttpClient<SearchHttpClient>(client =>
{
    client.BaseAddress = new Uri(baseUrl);
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});
builder.Services.AddScoped<ISearchBusiness, SearchBusiness>();

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