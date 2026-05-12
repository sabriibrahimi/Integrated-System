using CoursesApplication.Domain.Models;
using CoursesApplication.Web.Response;

namespace CoursesApplication.Service.Interface;

public interface IExamAttemptService
{
    Task<ExamAttempt> GetByIdNotNullAsync(Guid id);
    Task<ExamAttemptResponse> GetByIdWithQuestionsAsync(Guid id);

}