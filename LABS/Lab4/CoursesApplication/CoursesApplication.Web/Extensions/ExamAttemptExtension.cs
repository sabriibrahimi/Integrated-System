using CoursesApplication.Domain.Dto;
using CoursesApplication.Domain.Models;
using CoursesApplication.Web.Response;

namespace CoursesApplication.Web.Extensions;

public static class ExamAttemptExtensions
{
    public static ExamAttemptResponse ToResponse(
        this ExamAttempt attempt,
        List<AttemptQuestionDto> questions)
    {
        return new ExamAttemptResponse(
            attempt.Id,
            attempt.ExamId,
            attempt.StudentId,
            attempt.StartedAt,
            attempt.FinishedAt,
            questions
        );
    }
}