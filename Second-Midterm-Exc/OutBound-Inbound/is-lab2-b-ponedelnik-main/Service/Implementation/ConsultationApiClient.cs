using Domain.Configuration;
using Domain.Dto;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Service.Interface;

namespace Service.Implementation;

public class ConsultationApiClient : IConsultationApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ConsultationApiClient> _logger;
    private readonly ConsultationApiSettings _settings;

    public ConsultationApiClient(
        HttpClient httpClient,
        ILogger<ConsultationApiClient> logger,
        IOptions<ConsultationApiSettings> settings)
    {
        _httpClient = httpClient;
        _logger = logger;
        _settings = settings.Value;
    }

    public async Task<List<ConsultationCommentDto>> GetCommentsByConsultationIdAsync(Guid consultationId)
    {
        var request = new HttpRequestMessage(
            HttpMethod.Get,
            $"api/consultationcomments/byconsultation/{consultationId}/paged?pageNumber=1&pageSize=5");

        request.Headers.Add("X-Api-Key", _settings.ApiKey);

        var response = await _httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode();

        return new List<ConsultationCommentDto>();
    }
}