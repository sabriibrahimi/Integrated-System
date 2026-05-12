using HotelApplication.Web.Mappers;
using HotelApplication.Web.Request;
using HotelApplication.Web.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HotelApplication.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly RoomMapper _roomMapper;

    public RoomController(RoomMapper roomMapper)
    {
        _roomMapper = roomMapper;
    }
    
    [HttpGet]
    public async Task<List<RoomWithReservationsResponse>> GetAll()
    {
        return await _roomMapper.GetAllAsync();
    }
    // /api/Rooms/by-id/{id}
    [HttpGet("{id}")]
    [EnableRateLimiting("external-api")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _roomMapper.GetByIdNotNullAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] RoomRequest eventRequest)
    {
        var result = await _roomMapper.InsertAsync(eventRequest);
        return Ok(result);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] RoomRequest eventRequest)
    {
        var result = await _roomMapper.UpdateAsync(id, eventRequest);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var result = await _roomMapper.DeleteAsync(id);
        return Ok(result); 
    }
    
    [HttpGet("paged")]
    public async Task<PaginatedResponse<RoomResponse>> Paged([FromQuery] PaginatedRequest request)
    {
        return await _roomMapper.GetAllPagedAsync(request);
    }

// /api/Rooms/by-id/{id}
    [HttpGet("/api/external/room/{id}")]
    [EnableRateLimiting("external-api")]
    public async Task<IActionResult> GetExternalById([FromRoute] Guid id)
    {
        var result = await _roomMapper.GetByIdNotNullAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        
        return Ok(result);
    }

    


}