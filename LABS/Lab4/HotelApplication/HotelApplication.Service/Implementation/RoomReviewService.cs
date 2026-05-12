using HotelApplication.Domain.Configuration;
using HotelApplication.Domain.Dto;
using HotelApplication.Service.Interface;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HotelApplication.Service.Implementation;

public class RoomReviewService : IRoomReviewService
{
    private readonly IRoomReviewApiClient _roomReviewClient;
    private readonly ILogger<RoomReviewService> _logger;
    private readonly IMemoryCache _memoryCache;
    private readonly RoomReviewApiSettings _roomApiSettings;

    public RoomReviewService(IRoomReviewApiClient roomReviewClient, ILogger<RoomReviewService> logger, IMemoryCache memoryCache, IOptions<RoomReviewApiSettings> options)
    {
        _roomReviewClient = roomReviewClient;
        _logger = logger;
        _memoryCache = memoryCache;
        _roomApiSettings = options.Value;
    }

    public async Task<List<RoomReviewDto>> GetReviewRoomDataByIdAsync(Guid roomReviewId)
    {
        var cacheKey = $"room-api:{roomReviewId}";
        
        if (_memoryCache.TryGetValue(cacheKey, out List<RoomReviewDto>?  cached))
        {
            _logger.LogDebug(
                "Cache hit for room reviews. RoomId: {RoomId}", roomReviewId);
            return cached ?? [];
        }
        

        var apiData = await _roomReviewClient.GetFirstFiveRoomReviewsByAsync(roomReviewId);

        var reviews =  apiData?.Items ?? [];

        _memoryCache.Set(
            cacheKey,
            reviews,
            TimeSpan.FromMinutes(_roomApiSettings.CacheExpirationMinutes));

        return reviews;
    }
}