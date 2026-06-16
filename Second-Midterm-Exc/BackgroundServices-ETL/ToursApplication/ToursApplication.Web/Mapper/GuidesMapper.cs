using ExamsApplication.Web.Extensions;
using ExamsApplication.Web.Request;
using ExamsApplication.Web.Response;
using ToursApplication.Service.Interface;
using ToursApplication.Web.Extension;
using ToursApplication.Web.Request;
using ToursApplication.Web.Response;

namespace ToursApplication.Web.Mapper;

public class GuidesMapper
{
    private readonly IGuidesService _guidesService;

    public GuidesMapper(IGuidesService guidesService)
    {
        _guidesService = guidesService;
    }

    public async Task<GuidesResponse> GetByIdAsync(Guid id)
    {
        var entity = await _guidesService.GetByIdNotNullAsync(id);
        return entity.ToResponse();
    }

    public async Task<List<GuidesResponse>> GetAllAsync()
    {
        var entities = await _guidesService.GetAllAsync();
        return entities.ToResponse();
    }

    public async Task<GuidesResponse> CreateAsync(CreateOrUpdateGuidesRequest request)
    {
        var result = await _guidesService.CreateAsync(request.ToDto());
        return result.ToResponse();
    }

    public async Task<GuidesResponse> UpdateAsync(Guid id, CreateOrUpdateGuidesRequest request)
    {
        var result = await _guidesService.UpdateAsync(id, request.ToDto());
        return result.ToResponse();
    }

    public async Task<GuidesResponse> DeleteAsync(Guid id)
    {
        var result = await _guidesService.DeleteByIdAsync(id);
        return result.ToResponse();
    }

    public async Task<PaginatedResponse<GuidesResponse>> GetAllPagedAsync(PaginatedRequest request)
    {
        var result = await _guidesService.GetAllPagedAsync(request.PageNumber, request.PageSize);
        return result.ToPaginatedResponse(x => x.ToResponse());
    }
}
