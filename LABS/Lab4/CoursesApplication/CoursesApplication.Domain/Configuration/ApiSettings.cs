namespace CoursesApplication.Domain.Configuration;

public class ApiSettings
{
    public string BaseAddress { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public int TimeoutSeconds { get; set; } = 30;
    public int CacheExpirationMinutes { get; set; } = 30;
    public string BaseUrl { get; set; }
}