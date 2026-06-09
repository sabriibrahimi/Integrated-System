using ExamsApplication.Web.Extensions;
using ExamsApplication.Web.Request;
using ExamsApplication.Web.Response;
using ToursApplication.Service.Interface;
using ToursApplication.Web.Extension;
using ToursApplication.Web.Request;
using ToursApplication.Web.Response;

namespace ToursApplication.Web.Mapper;

public class TourMapper
{
    private readonly ITourService _tourService;

    public TourMapper(ITourService tourService)
    {
        _tourService = tourService;
    }

    public async Task<TourResponse> GetTourByIdAsync(Guid id)
    {
        var tour = await _tourService.GetByIdNotNullAsync(id);
        return tour.ToResponse();
    }

    public async Task<List<TourResponse>> GetAllAsync()
    {
        var tours = await _tourService.GetAllAsync();
        return tours.ToResponse();
    }

    public async Task<TourResponse> CreateAsync(CreateOrUpdateTourRequest request)
    {
        var result = await _tourService.CreateAsync(request.ToDto());
        return result.ToResponse();
    }

    public async Task<TourResponse> UpdateAsync(Guid id, CreateOrUpdateTourRequest request)
    {
        var result = await _tourService.UpdateAsync(id, request.ToDto());
        return result.ToResponse();
    }

    public async Task<PaginatedResponse<TourResponse>> GetAllPagedAsync(PaginatedRequest request)
    {
        var result = await _tourService.GetAllPagedAsync(request.PageNumber, request.PageSize);
        return result.ToPaginatedResponse(x => x.ToResponse());
    }
}