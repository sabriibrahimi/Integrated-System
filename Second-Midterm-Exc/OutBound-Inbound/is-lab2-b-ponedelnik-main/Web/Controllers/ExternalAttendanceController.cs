using System.Text.Json;
using Domain.Requests;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace Web.Controllers;

[ApiController]
[Route("api/external/attendance")]
public class ExternalAttendanceController : ControllerBase
{
    private readonly IInboundAttendanceService _inboundAttendanceService;

    public ExternalAttendanceController(
        IInboundAttendanceService inboundAttendanceService)
    {
        _inboundAttendanceService = inboundAttendanceService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] InboundAttendanceRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        
        var apiClientId = Guid.NewGuid();

        var rawPayload = JsonSerializer.Serialize(request);

        var id = await _inboundAttendanceService.CreateAsync(
            rawPayload,
            apiClientId);

        return Accepted(new { Id = id });
    }

    [HttpGet("{id:guid}/status")]
    public async Task<IActionResult> Status(
        [FromRoute] Guid id)
    {
        var entry =
            await _inboundAttendanceService.GetByIdAsync(id);

        if (entry == null)
        {
            return NotFound();
        }

        return Ok(new
        {
            entry.Id,
            entry.Status,
            entry.ReceivedAt,
            entry.ProcessedAt,
            entry.ErrorMessage,
            entry.CreatedAttendanceId
        });
    }
}