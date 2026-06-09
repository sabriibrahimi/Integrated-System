using ExamsApplication.Web.Request;
using ExamsApplication.Web.Response;
using Microsoft.AspNetCore.Mvc;
using ToursApplication.Web.Mapper;
using ToursApplication.Web.Request;
using ToursApplication.Web.Response;

namespace ToursApplication.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TravelAgencyController : ControllerBase
{
    private readonly TravelAgencyMapper _travelAgencyMapper;

    public TravelAgencyController(TravelAgencyMapper travelAgencyMapper)
    {
        _travelAgencyMapper = travelAgencyMapper;
    }

    [HttpGet("")]
    public async Task<List<TravelAgencyResponse>> GetAllAsync()
    {
        return await _travelAgencyMapper.GetAllAsync();
    }

    [HttpGet("paged")]
    public async Task<PaginatedResponse<TravelAgencyResponse>> GetAllPagedAsync([FromQuery] PaginatedRequest request)
    {
        return await _travelAgencyMapper.GetAllPagedAsync(request);
    }

    [HttpGet("{id}")]
    public async Task<TravelAgencyResponse> GetByIdAsync([FromRoute] Guid id)
    {
        return await _travelAgencyMapper.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateOrUpdateTravelAgencyRequest request)
    {
        var result = await _travelAgencyMapper.CreateAsync(request);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] CreateOrUpdateTravelAgencyRequest request)
    {
        var result = await _travelAgencyMapper.UpdateAsync(id, request);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var result = await _travelAgencyMapper.DeleteAsync(id);
        return Ok(result);
    }
}
