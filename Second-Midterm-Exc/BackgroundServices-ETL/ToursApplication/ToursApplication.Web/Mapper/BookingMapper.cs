using ExamsApplication.Web.Extensions;
using ExamsApplication.Web.Request;
using ExamsApplication.Web.Response;
using ToursApplication.Service.Interface;
using ToursApplication.Web.Extension;
using ToursApplication.Web.Request;
using ToursApplication.Web.Response;

namespace ToursApplication.Web.Mapper;

public class BookingMapper
{
    private readonly IBookingService _bookingService;

    public BookingMapper(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    public async Task<BookingResponse> GetByIdAsync(Guid id)
    {
        var entity = await _bookingService.GetByIdNotNullAsync(id);
        return entity.ToResponse();
    }

    public async Task<List<BookingResponse>> GetAllAsync()
    {
        var entities = await _bookingService.GetAllAsync();
        return entities.ToResponse();
    }

    public async Task<BookingResponse> CreateAsync(CreateOrUpdateBookingRequest request)
    {
        var result = await _bookingService.CreateAsync(request.ToDto());
        return result.ToResponse();
    }

    public async Task<BookingResponse> UpdateAsync(Guid id, CreateOrUpdateBookingRequest request)
    {
        var result = await _bookingService.UpdateAsync(id, request.ToDto());
        return result.ToResponse();
    }

    public async Task<BookingResponse> DeleteAsync(Guid id)
    {
        var result = await _bookingService.DeleteByIdAsync(id);
        return result.ToResponse();
    }

    public async Task<PaginatedResponse<BookingResponse>> GetAllPagedAsync(PaginatedRequest request)
    {
        var result = await _bookingService.GetAllPagedAsync(request.PageNumber, request.PageSize);
        return result.ToPaginatedResponse(x => x.ToResponse());
    }
}
