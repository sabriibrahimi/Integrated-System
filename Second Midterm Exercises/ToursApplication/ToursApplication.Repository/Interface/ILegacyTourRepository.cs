using ToursApplication.Domain.Models;

namespace ToursApplication.Repository.Interface;

public interface ILegacyTourRepository
{
    Task<List<Tour>> GetTourAsync();
    Task<List<Offers>> GetOffersAsync();
}