using ExamsApplication.Web.Request;
using ExamsApplication.Web.Response;
using Microsoft.AspNetCore.Mvc;
using ToursApplication.Web.Mapper;
using ToursApplication.Web.Request;
using ToursApplication.Web.Response;

namespace ToursApplication.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OffersController : ControllerBase
{
    private readonly OffersMapper _offersMapper;

    public OffersController(OffersMapper offersMapper)
    {
        _offersMapper = offersMapper;
    }

    [HttpGet("")]
    public async Task<List<OffersResponse>> GetAllAsync()
    {
        return await _offersMapper.GetAllAsync();
    }

    [HttpGet("paged")]
    public async Task<PaginatedResponse<OffersResponse>> GetAllPagedAsync([FromQuery] PaginatedRequest request)
    {
        return await _offersMapper.GetAllPagedAsync(request);
    }

    [HttpGet("{id}")]
    public async Task<OffersResponse> GetByIdAsync([FromRoute] Guid id)
    {
        return await _offersMapper.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateOrUpdateOffersRequest request)
    {
        var result = await _offersMapper.CreateAsync(request);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] CreateOrUpdateOffersRequest request)
    {
        var result = await _offersMapper.UpdateAsync(id, request);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var result = await _offersMapper.DeleteAsync(id);
        return Ok(result);
    }
}
