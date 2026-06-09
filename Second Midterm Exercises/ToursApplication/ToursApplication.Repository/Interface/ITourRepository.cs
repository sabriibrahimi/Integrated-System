using ToursApplication.Domain.Models;

namespace ToursApplication.Repository.Interface;

public interface ITourRepository
{
    Task BulkInsertOrUpdateToursAsync(List<Tour> tours);
    Task BulkInsertOrUpdateOffersAsync(List<Offers> offers);
}