using ExamsApplication.Web.Request;
using ExamsApplication.Web.Response;
using Microsoft.AspNetCore.Mvc;
using ToursApplication.Web.Mapper;
using ToursApplication.Web.Request;
using ToursApplication.Web.Response;

namespace ToursApplication.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly BookingMapper _bookingMapper;

    public BookingController(BookingMapper bookingMapper)
    {
        _bookingMapper = bookingMapper;
    }

    [HttpGet("")]
    public async Task<List<BookingResponse>> GetAllAsync()
    {
        return await _bookingMapper.GetAllAsync();
    }

    [HttpGet("paged")]
    public async Task<PaginatedResponse<BookingResponse>> GetAllPagedAsync([FromQuery] PaginatedRequest request)
    {
        return await _bookingMapper.GetAllPagedAsync(request);
    }

    [HttpGet("{id}")]
    public async Task<BookingResponse> GetByIdAsync([FromRoute] Guid id)
    {
        return await _bookingMapper.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateOrUpdateBookingRequest request)
    {
        var result = await _bookingMapper.CreateAsync(request);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] CreateOrUpdateBookingRequest request)
    {
        var result = await _bookingMapper.UpdateAsync(id, request);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var result = await _bookingMapper.DeleteAsync(id);
        return Ok(result);
    }
}
