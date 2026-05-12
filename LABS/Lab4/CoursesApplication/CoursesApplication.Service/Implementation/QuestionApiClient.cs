using System.Net.Http.Json;
using CoursesApplication.Domain.Configuration;
using CoursesApplication.Domain.Dto;
using CoursesApplication.Service.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CoursesApplication.Service.Implementation;

public class QuestionApiClient  : IQuestionApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<QuestionApiClient> _logger;
    private readonly ApiSettings _settings;

    public QuestionApiClient( HttpClient httpClient,  ILogger<QuestionApiClient> logger,  IOptions<ApiSettings> options)
    {
        _httpClient = httpClient;
        _logger = logger;
        _settings = options.Value;
    }
    
    public async Task<List<AttemptQuestionDto>> GetFirstFiveQuestionsWithAttemptAsync(Guid attemptId)
    {
        var url = $"api/attemptquestions/byattempt/{attemptId}/paged?page=1&pageSize=5";
        
        _logger.LogInformation("Fetching first 5 question for attempt {AttemptId}", attemptId);

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning("External question system returned status {StatusCode} for attempt {AttemptId}", (int)response.StatusCode, attemptId);
            return [] ;
        }
        var result = await response.Content.ReadFromJsonAsync<PaginatedResult<AttemptQuestionDto>>();
        return result?.Items ?? [];
    }
}