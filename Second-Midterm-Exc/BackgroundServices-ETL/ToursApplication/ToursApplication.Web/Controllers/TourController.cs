using ExamsApplication.Web.Request;
using ExamsApplication.Web.Response;
using Microsoft.AspNetCore.Mvc;
using ToursApplication.Web.Mapper;
using ToursApplication.Web.Request;
using ToursApplication.Web.Response;

namespace ToursApplication.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TourController : ControllerBase
{
    private readonly TourMapper _tourMapper;

    public TourController(TourMapper tourMapper)
    {
        _tourMapper = tourMapper;
    }

    [HttpGet("")]
    public async Task<List<TourResponse>> GetAllAsync()
    {
        return await _tourMapper.GetAllAsync();
    }

    [HttpGet("paged")]
    public async Task<PaginatedResponse<TourResponse>> GetAllPagedAsync([FromQuery] PaginatedRequest request)
    {
        return await _tourMapper.GetAllPagedAsync(request);
    }

    [HttpGet("{id}")]
    public async Task<TourResponse> GetByIdAsync([FromRoute] Guid id)
    {
        return await _tourMapper.GetTourByIdAsync(id);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateOrUpdateTourRequest request)
    {
        var result = await _tourMapper.CreateAsync(request);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] CreateOrUpdateTourRequest request)
    {
        var result = await _tourMapper.UpdateAsync(id, request);
        return Ok(result);
    }
}
