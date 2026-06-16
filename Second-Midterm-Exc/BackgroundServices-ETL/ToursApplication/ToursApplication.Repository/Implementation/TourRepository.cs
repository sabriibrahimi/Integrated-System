using ExamsApplication.Repository;
using ToursApplication.Domain.Models;
using ToursApplication.Repository.Interface;
using EFCore.BulkExtensions;

namespace ToursApplication.Repository.Implementation;

public class TourRepository : ITourRepository
{
    private readonly ApplicationDbContext _context;

    public TourRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task BulkInsertOrUpdateToursAsync(List<Tour> tours)
    {
        await _context.BulkInsertOrUpdateAsync(tours);
    }

    public async Task BulkInsertOrUpdateOffersAsync(List<Offers> offers)
    {
        await _context.BulkInsertOrUpdateAsync(offers);
    }
}