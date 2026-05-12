using System.Net.Http.Json;
using HotelApplication.Domain.Configuration;
using HotelApplication.Domain.Dto;
using HotelApplication.Service.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HotelApplication.Service.Implementation;

public class RoomReviewApiClient  : IRoomReviewApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<RoomReviewApiClient> _logger;
    private readonly RoomReviewApiSettings _settings;

    public RoomReviewApiClient(HttpClient httpClient, ILogger<RoomReviewApiClient> logger, IOptions<RoomReviewApiSettings> settings)
    {
        _httpClient = httpClient;
        _logger = logger;
        _settings = settings.Value;
    }

    public async Task<ExternalPagedResponse<RoomReviewDto>> GetFirstFiveRoomReviewsByAsync(Guid roomId)
    {
        var apiKey = _settings.ApiKey;
        var url = $"https://integriranisistemi.finki.ukim.mk/api/roomreviews/byroom/{roomId}/paged?page=1&pageSize=5";
        
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        
        var reviewData = await response.Content.ReadFromJsonAsync<ExternalPagedResponse<RoomReviewDto>>();

        return reviewData;
    }
}