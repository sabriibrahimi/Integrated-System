using CoursesApplication.Domain.Models;
using CoursesApplication.Repository.Interface;
using CoursesApplication.Service.Interface;
using CoursesApplication.Web.Response;

namespace CoursesApplication.Service.Implementation;

public class ExamAttemptService : IExamAttemptService
{
    private readonly IRepository<ExamAttempt> _repository;
    private readonly IQuestionApiClient _questionApiClient;

    public ExamAttemptService(
        IRepository<ExamAttempt> repository,
        IQuestionApiClient questionApiClient)
    {
        _repository = repository;
        _questionApiClient = questionApiClient;
    }
    
    public async Task<ExamAttempt> GetByIdNotNullAsync(Guid id)
    {
        var result = await _repository.Get(
            selector: x => x,
            predicate: x => x.Id == id);

        if (result == null)
        {
            throw new Exception();
        }

        return result;
    }

    public async Task<ExamAttemptResponse> GetByIdWithQuestionsAsync(Guid id)
    {
        var attempt = await GetByIdNotNullAsync(id);

        var questions = await _questionApiClient
            .GetFirstFiveQuestionsWithAttemptAsync(id);

        return new ExamAttemptResponse(
            attempt.Id,
            attempt.ExamId,
            attempt.StudentId,
            attempt.StartedAt,
            attempt.FinishedAt,
            questions);
    }
}
