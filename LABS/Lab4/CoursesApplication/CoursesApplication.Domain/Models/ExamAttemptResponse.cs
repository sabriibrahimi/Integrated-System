using CoursesApplication.Domain.Dto;

namespace CoursesApplication.Web.Response;

public record ExamAttemptResponse(
    Guid Id,
    Guid ExamId,
    Guid StudentId,
    DateTime StartedAt,
    DateTime? FinishedAt,

    List<AttemptQuestionDto> Questions
);