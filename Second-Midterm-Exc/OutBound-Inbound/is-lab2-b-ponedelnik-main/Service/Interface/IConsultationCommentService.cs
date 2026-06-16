using Domain.Dto;

namespace Service.Interface;

public interface IConsultationCommentService
{
    Task<List<ConsultationCommentDto>> GetCommentsForConsultationAsync(Guid consultationId);

}