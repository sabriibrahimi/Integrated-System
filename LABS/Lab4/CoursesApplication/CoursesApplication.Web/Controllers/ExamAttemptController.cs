using CoursesApplication.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CoursesApplication.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExamAttemptController : ControllerBase
{
    private readonly IExamAttemptService _examAttemptService;

    public ExamAttemptController(IExamAttemptService examAttemptService)
    {
        _examAttemptService = examAttemptService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _examAttemptService.GetByIdWithQuestionsAsync(id);

        return Ok(result);
    }
}