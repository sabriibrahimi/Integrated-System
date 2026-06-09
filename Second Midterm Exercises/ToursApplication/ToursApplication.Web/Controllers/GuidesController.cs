using ExamsApplication.Web.Request;
using ExamsApplication.Web.Response;
using Microsoft.AspNetCore.Mvc;
using ToursApplication.Web.Mapper;
using ToursApplication.Web.Request;
using ToursApplication.Web.Response;

namespace ToursApplication.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GuidesController : ControllerBase
{
    private readonly GuidesMapper _guidesMapper;

    public GuidesController(GuidesMapper guidesMapper)
    {
        _guidesMapper = guidesMapper;
    }

    [HttpGet("")]
    public async Task<List<GuidesResponse>> GetAllAsync()
    {
        return await _guidesMapper.GetAllAsync();
    }

    [HttpGet("paged")]
    public async Task<PaginatedResponse<GuidesResponse>> GetAllPagedAsync([FromQuery] PaginatedRequest request)
    {
        return await _guidesMapper.GetAllPagedAsync(request);
    }

    [HttpGet("{id}")]
    public async Task<GuidesResponse> GetByIdAsync([FromRoute] Guid id)
    {
        return await _guidesMapper.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateOrUpdateGuidesRequest request)
    {
        var result = await _guidesMapper.CreateAsync(request);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] CreateOrUpdateGuidesRequest request)
    {
        var result = await _guidesMapper.UpdateAsync(id, request);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var result = await _guidesMapper.DeleteAsync(id);
        return Ok(result);
    }
}
