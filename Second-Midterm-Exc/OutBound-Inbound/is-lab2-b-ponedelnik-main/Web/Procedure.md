# PART 1: Integration with an External System — 30 points

### Create Settings in appsettings.json
```C#
"WeatherApi": {
    "BaseAddress": "https://api.openweathermap.org/data/2.5/",
    "TimeoutSeconds": 30,
    "CacheExpirationMinutes": 30,
    "ApiKey": "PLACEHOLDER"
},
```

### Settings class
```C#
public class WeatherApiSettings
{
  public string BaseAddress { get; set; } = string.Empty;
  public string ApiKey { get; set; } = string.Empty;
  public int TimeoutSeconds { get; set; } = 30;
  public int CacheExpirationMinutes { get; set; } = 30;
}
```

### Connect them in program cs
```C#
builder.Services.Configure<WeatherApiSettings>(
    builder.Configuration.GetSection("WeatherApi") 
);
```

### Add secret key
```
dotnet user-secrets init
dotnet user-secrets set "WeatherApi:ApiKey" "your_real_api_key"
```

### Create the response of the api
```C#
public class WeatherApiResponse
{
  [JsonPropertyName("id")]
  public int Id { get; set; }

  [JsonPropertyName("main")]
  public WeatherMain MainWeatherData { get; set; }
  
  [JsonPropertyName("wind")]
  public WeatherWind Wind { get; set; }
}

///

public class WeatherMain
{
  [JsonPropertyName("temp")]
  public decimal Temperature {get; set;}

  [JsonPropertyName("feels_like")]
  public decimal FeelsLike { get; set; }
  
  [JsonPropertyName("temp_min")]
  public decimal MinimumTemperature {get; set;}
  
  [JsonPropertyName("temp_max")]
  public decimal MaximumTemperature {get; set;}
}

///

public class WeatherWind
{
  [JsonPropertyName("speed")]
  public decimal Speed { get; set; }

  [JsonPropertyName("deg")]
  public int Degress { get; set; }
}
```
  
### Create the dto
```C#
public class EventWeatherDto
{
    public double Temperature { get; set; }
    public double FeelsLike { get; set; }
    public double TempMin { get; set; }
    public double TempMax { get; set; }
    public int Humidity { get; set; }
    public double WindSpeed { get; set; }
    public string Condition { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public DateTime RetrievedAt { get; set; }
}
```
  
### You create 2 interfaces 
One for client making http requests to the outbound system. One applying the business logic of the data
```C#
//Client
public interface IWeatherApiClient
{
    Task<EventWeatherDto> GetWeatherForecastForCityAndCountry(string city, string country);
}

//Service
public interface IWeatherService
{
    Task<EventWeatherDto> GetWeatherDataForEventIdAsync(Guid eventId);
}
```

### Implement ...ApiClient
```C#
public class WeatherApiClient : IWeatherApiClient
{
    
    private readonly HttpClient _httpClient;
    private readonly ILogger<WeatherApiClient> _logger;
    private readonly WeatherApiSettings _settings;

    public WeatherApiClient(HttpClient httpClient, ILogger<WeatherApiClient> logger, IOptions<WeatherApiSettings> settings)
    {
        _httpClient = httpClient;
        _logger = logger;
        _settings = settings.Value;
    }
    
    public async Task<EventWeatherDto> GetWeatherForecastForCityAndCountry(string city, string country)
    {
        //weather?q=Skopje,MK&appid=8839f3801e1b62830dc35d17e5fa76cf
        var apiKey = _settings.ApiKey;
        var url = $"weather?q={city},{country}&appid={apiKey}";

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var weatherData = await response.Content.ReadFromJsonAsync<WeatherApiResponse>();

        return new EventWeatherDto()
        {
            Temperature = (double)weatherData.MainWeatherData.Temperature,
            FeelsLike = (double)weatherData.MainWeatherData.FeelsLike,
            TempMax = (double)weatherData.MainWeatherData.MaximumTemperature,
            TempMin = (double)weatherData.MainWeatherData.MinimumTemperature,
            Humidity = 1,
            WindSpeed = (double)weatherData.Wind.Speed,
            Condition = "",
            Description = "",
            Icon = "",
            RetrievedAt = DateTime.Now,
        };
    }
}
```

### Add the client in `Program.cs`
```C#
builder.Services.AddHttpClient<IWeatherApiClient, WeatherApiClient>((sp, client) =>
{

    var settings = sp.GetRequiredService<IOptions<WeatherApiSettings>>();

    client.BaseAddress = new Uri(settings.Value.BaseAddress);
    client.Timeout = TimeSpan.FromSeconds(settings.Value.TimeoutSeconds);
    client.DefaultRequestHeaders.Add("X-Api-Key", settings.ApiKey);
});
```

### Add the cache in `Program.cs`
```csharp
builder.Services.AddMemoryCache();
```

### Implement ...Service
```C#
public class WeatherService : IWeatherService
{
    private readonly IEventService _eventService;
    private readonly IWeatherApiClient _weatherApiClient;
    private readonly ILogger<WeatherService> _logger;
    private readonly IMemoryCache _memoryCache;
    private readonly WeatherApiSettings _weatherApiSettings;

    public WeatherService(IEventService eventService, IWeatherApiClient weatherApiClient, ILogger<WeatherService> logger, IMemoryCache memoryCache, IOptions<WeatherApiSettings> weatherApiSettings)
    {
        _eventService = eventService;
        _weatherApiClient = weatherApiClient;
        _logger = logger;
        _memoryCache = memoryCache;
        _weatherApiSettings = weatherApiSettings.Value;
    }

    public async Task<EventWeatherDto> GetWeatherDataForEventIdAsync(Guid eventId)
    {
        // Find city and country
        var eventData = await _eventService.GetByIdAsync(eventId);

        var city = eventData.Venue.City;
        var country = eventData.Venue.Country;
        
        // Construct cache key
        var cacheKey = $"weather-api:{city}:{country}";

        // Check cache
        if (_memoryCache.TryGetValue(cacheKey, out EventWeatherDto? cached))
        {
            _logger.LogDebug(
                "Cache hit for event {EventId}", eventId);
            return cached;
        }
        
        // If present, return
        if (cached != null)
        {
            return cached;
        }

        // If not present, fetch
        var apiData = 
            await _weatherApiClient.GetWeatherForecastForCityAndCountry(city, country);

        // Put in the cache
        _memoryCache.Set(cacheKey, apiData, TimeSpan.FromMinutes(_weatherApiSettings.CacheExpirationMinutes));
        
        _logger.LogInformation(
            "Weather cached for event {EventId}: " +
            "{Condition}, {Temp}°C",
            eventId, apiData.Condition, apiData.Temperature);
        
        return apiData;

    }
}
```

### Add the dto to the response and in the mapper add the `...Service`

# PART 2: Application Security — 20 points

### Create an Api Client
```C#
public class ApiClient : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public int RateLimitMinutes { get; set; }
}
```

### Add Api Client to `DbContext`
```csharp
public DbSet<ApiClient> ApiClients { get; set; }
```

### Add evolve migrations
```sql
CREATE TABLE IF NOT EXISTS "ApiClients" (
    "Id" TEXT NOT NULL PRIMARY KEY,
    "Name" TEXT NOT NULL,
    "ApiKey" TEXT NOT NULL,
    "IsActive" INTEGER NOT NULL,
    "RateLimitMinutes" INTEGER NOT NULL
);

INSERT INTO "ApiClients" (
    "Id",
    "Name",
    "ApiKey",
    "IsActive",
    "RateLimitMinutes"
)
VALUES (
   '99999999-9999-9999-9999-999999999999',
   'External Partner',
   'sabrisabrisabrisabri12',
   1,
   60
);
```

### Create a Middleware 
```csharp
public class ApiKeyAuthMiddleware
{
    private readonly RequestDelegate _next;

    public ApiKeyAuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ApplicationDbContext dbContext)
    {
        if (!context.Request.Path.StartsWithSegments("/api/external"))
        {
            await _next(context);
            return;
        }

        if (!context.Request.Headers.TryGetValue("X-Api-Key", out var authHeader))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsJsonAsync(new
            {
                Error = "Api Key is Required"
            });
        }
        
        Console.WriteLine(authHeader);
        
        var client = dbContext.ApiClients.FirstOrDefault(
            x => x.ApiKey == authHeader.ToString() && x.IsActive);

        if (client == null)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsJsonAsync(new
            {
                Error = "Api Key is not valid"
            });
        }

        context.Items["ApiClient"] = client;
        
        await _next(context);
    }
}
```

### Register middleware
```csharp
app.UseMiddleware<ApiKeyAuthMiddleware>();
```

### Add the rate limiting in `Program.cs`
```csharp
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = 429;

    options.AddPolicy("external-api", context =>
    {
        var apiKey = context.Request.Headers["x-api-key"];

        var apiClient = context.Items["ApiClient"] as ApiClient;

        return RateLimitPartition.GetFixedWindowLimiter(apiKey.ToString(), _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 60,
            Window = TimeSpan.FromMinutes(1),
            QueueLimit = 0,
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst
        });
    });
});

//Test limiter
builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy("external api", context =>
    {
        return RateLimitPartition.GetFixedWindowLimiter("Test", _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 60,
            QueueLimit = 0,
            Window = TimeSpan.FromDays(1),
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst
        });
    });
});
```

### Register the rate limiting (it needs to be after the middleware reg)
```csharp
app.UseMiddleware<ApiKeyAuthMiddleware>();
app.UseRateLimiter();
```

### For rate limiting to be applied you need this
```csharp
[EnableRateLimiting("external-api")]
[HttpGet]
public IActionResult GetExternalData()
{
    return Ok();
}
```

# PART 3: Accepting External Calls — Inbound REST — 30 points

### Create the inbound requests
```csharp
public record InboundRoomRequest(
    string HotelId,
    string RoomNumber,
    int Capacity,
    decimal PricePerNight,
    string Status
);

// {
// "hotelId": "string",
// "roomNumber": "string",
// "capacity": "integer",
// "pricePerNight": "decimal",
// "status": "string"
// }
```


### Create external Controller (no logic for now)
```csharp
[ApiController]
[Route("api/external/room")]
public class ExternalController : ControllerBase
{
    [HttpPost]
    [EnableRateLimiting("external-api")]
    public async Task<IActionResult> Create([FromBody] InboundRoomRequest request)
    {
        return Ok();
    }
    
    [HttpGet("{id}/status")]
    [EnableRateLimiting("external-api")]
    public async Task<IActionResult> Status([FromRoute] Guid id)
    {
        return Ok();
    }
}
```

### Create the `InboundEntry` model
```csharp
public class InboundAttendanceEntries
{
    public string? RawPayload { get; set; }
    
    public Guid ApiClientId { get; set; }
    public virtual ApiClient ApiClient { get; set; } = null!;
    
    public DateTime ReceivedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public string? ErrorMessage { get; set; }
    public Guid? CreatedRoomId { get; set; }
}
```

### Add `InboundEntry` to `DbContext`
```csharp
public DbSet<InboundAttendanceEntries> InboundAttendanceEntries { get; set; }
```

### Create an Inbound Service
Which is used to save and get an InboundEntry

### Create an Inbound Processing Service
Which is used to process pending inbound entries

### Create the quartz Job to trigger the processing service


### Jobs examples
```csharp
public class ReservationCleanupBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<ReservationCleanupBackgroundService> _logger;

    public ReservationCleanupBackgroundService(IServiceScopeFactory serviceScopeFactory,
        ILogger<ReservationCleanupBackgroundService> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var reservationService = scope.ServiceProvider.GetRequiredService<IReservationService>();
            
            _logger.LogInformation("Reservation cleanup job started...");
            
            var reservations = await reservationService.GetAllByDateReservedSince(DateTime.UtcNow.AddMinutes(-15));

            _logger.LogInformation("Fetched total {reservationCount} reservations", reservations.Count);

            foreach (var reservation in reservations)
            {
                try
                {
                    _logger.LogInformation("Expiring reservation with ID: {reservationId}", reservation.Id);
                    await reservationService.ExpireAsync(reservation);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while expiring reservation with ID {reservationId}", reservation.Id);
                }
            }
            
            _logger.LogInformation("Reservation cleanup job finished succesfully...");
            
            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}

////
////Register it
builder.Services.AddHostedService<ReservationCleanupBackgroundService>();
```

---

```csharp
public class QuartzReservationCleanupJob : IJob
{
    private readonly IReservationService _reservationService;
    private readonly ILogger<QuartzReservationCleanupJob> _logger;

    public QuartzReservationCleanupJob(IReservationService reservationService, ILogger<QuartzReservationCleanupJob> logger)
    {
        _reservationService = reservationService;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Reservation cleanup job started...");

        var reservations = await _reservationService.GetAllByDateReservedSince(DateTime.Now.AddMinutes(-15));

        _logger.LogInformation($"Fetched total {reservations.Count} reservations");

        foreach (var reservation in reservations)
        {
            await _reservationService.ExpireAsync(reservation);
            _logger.LogInformation($"Reservation {reservation.Id} has been cleared");
        }
        
        _logger.LogInformation("Reservation cleanup job finished...");
    }
}

//Register


builder.Services.AddQuartzHostedService();

builder.Services.AddQuartz(options =>
{
    var jobKey = new JobKey("reservation-cleanup", "maintenance");

    options.AddJob<QuartzReservationCleanupJob>(o =>
        o.WithIdentity(jobKey));

    options.AddTrigger(o =>
    {
        o.ForJob(jobKey)
            .WithIdentity("reservation-cleanup-trigger")
            .WithCronSchedule("0 0/1 * * * ?")
            .WithDescription("Expires unpaid reservations");
    });
});
```

