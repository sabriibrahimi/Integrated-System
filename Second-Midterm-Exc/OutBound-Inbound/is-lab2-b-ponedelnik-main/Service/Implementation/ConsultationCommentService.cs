using Domain.Dto;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Service.Interface;

namespace Service.Implementation;

public class ConsultationCommentService : IConsultationCommentService
{
    
    private readonly IConsultationApiClient _consultationApiClient;
    private readonly ILogger<ConsultationCommentService> _logger;
    private readonly IMemoryCache _memoryCache;

    public ConsultationCommentService(IConsultationApiClient consultationApiClient, ILogger<ConsultationCommentService> logger, IMemoryCache memoryCache)
    {
        _consultationApiClient = consultationApiClient;
        _logger = logger;
        _memoryCache = memoryCache;
    }

    public async Task<List<ConsultationCommentDto>> GetCommentsForConsultationAsync(Guid consultationId)
    {
         _logger.LogInformation(
             "Fetching comments for consultations"
             );

             var cacheKey = $"consultation-comments:{consultationId}";

             if (_memoryCache.TryGetValue(cacheKey,
                     out List<ConsultationCommentDto>? cachedComments))
             {
                 return cachedComments!;
             }

             var comments =
                 await _consultationApiClient.GetCommentsByConsultationIdAsync(consultationId);

             _memoryCache.Set(
                 cacheKey,
                 comments,
                 TimeSpan.FromHours(1));

             return comments;
    }
    
}