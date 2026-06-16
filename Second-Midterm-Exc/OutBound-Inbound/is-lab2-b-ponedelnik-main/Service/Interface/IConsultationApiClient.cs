using Domain.Dto;

namespace Service.Interface;

public interface IConsultationApiClient
{
    Task<List<ConsultationCommentDto>> GetCommentsByConsultationIdAsync(Guid consultationId);

}