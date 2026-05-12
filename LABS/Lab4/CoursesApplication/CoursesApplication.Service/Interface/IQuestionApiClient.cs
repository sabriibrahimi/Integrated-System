using CoursesApplication.Domain.Dto;

namespace CoursesApplication.Service.Interface;

public interface IQuestionApiClient
{
    Task<List<AttemptQuestionDto>> GetFirstFiveQuestionsWithAttemptAsync(Guid attemptId);
}