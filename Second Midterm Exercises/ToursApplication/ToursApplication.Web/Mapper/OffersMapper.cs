using ExamsApplication.Web.Extensions;
using ExamsApplication.Web.Request;
using ExamsApplication.Web.Response;
using ToursApplication.Service.Interface;
using ToursApplication.Web.Extension;
using ToursApplication.Web.Request;
using ToursApplication.Web.Response;

namespace ToursApplication.Web.Mapper;

public class OffersMapper
{
    private readonly IOffersService _offersService;

    public OffersMapper(IOffersService offersService)
    {
        _offersService = offersService;
    }

    public async Task<OffersResponse> GetByIdAsync(Guid id)
    {
        var entity = await _offersService.GetByIdNotNullAsync(id);
        return entity.ToResponse();
    }

    public async Task<List<OffersResponse>> GetAllAsync()
    {
        var entities = await _offersService.GetAllAsync();
        return entities.ToResponse();
    }

    public async Task<OffersResponse> CreateAsync(CreateOrUpdateOffersRequest request)
    {
        var result = await _offersService.CreateAsync(request.ToDto());
        return result.ToResponse();
    }

    public async Task<OffersResponse> UpdateAsync(Guid id, CreateOrUpdateOffersRequest request)
    {
        var result = await _offersService.UpdateAsync(id, request.ToDto());
        return result.ToResponse();
    }

    public async Task<OffersResponse> DeleteAsync(Guid id)
    {
        var result = await _offersService.DeleteByIdAsync(id);
        return result.ToResponse();
    }

    public async Task<PaginatedResponse<OffersResponse>> GetAllPagedAsync(PaginatedRequest request)
    {
        var result = await _offersService.GetAllPagedAsync(request.PageNumber, request.PageSize);
        return result.ToPaginatedResponse(x => x.ToResponse());
    }
}
