using ExamsApplication.Web.Extensions;
using ExamsApplication.Web.Request;
using ExamsApplication.Web.Response;
using ToursApplication.Service.Interface;
using ToursApplication.Web.Extension;
using ToursApplication.Web.Request;
using ToursApplication.Web.Response;

namespace ToursApplication.Web.Mapper;

public class TravelAgencyMapper
{
    private readonly ITravelAgencyService _travelAgencyService;

    public TravelAgencyMapper(ITravelAgencyService travelAgencyService)
    {
        _travelAgencyService = travelAgencyService;
    }

    public async Task<TravelAgencyResponse> GetByIdAsync(Guid id)
    {
        var entity = await _travelAgencyService.GetByIdNotNullAsync(id);
        return entity.ToResponse();
    }

    public async Task<List<TravelAgencyResponse>> GetAllAsync()
    {
        var entities = await _travelAgencyService.GetAllAsync();
        return entities.ToResponse();
    }

    public async Task<TravelAgencyResponse> CreateAsync(CreateOrUpdateTravelAgencyRequest request)
    {
        var result = await _travelAgencyService.CreateAsync(request.ToDto());
        return result.ToResponse();
    }

    public async Task<TravelAgencyResponse> UpdateAsync(Guid id, CreateOrUpdateTravelAgencyRequest request)
    {
        var result = await _travelAgencyService.UpdateAsync(id, request.ToDto());
        return result.ToResponse();
    }

    public async Task<TravelAgencyResponse> DeleteAsync(Guid id)
    {
        var result = await _travelAgencyService.DeleteByIdAsync(id);
        return result.ToResponse();
    }

    public async Task<PaginatedResponse<TravelAgencyResponse>> GetAllPagedAsync(PaginatedRequest request)
    {
        var result = await _travelAgencyService.GetAllPagedAsync(request.PageNumber, request.PageSize);
        return result.ToPaginatedResponse(x => x.ToResponse());
    }
}
