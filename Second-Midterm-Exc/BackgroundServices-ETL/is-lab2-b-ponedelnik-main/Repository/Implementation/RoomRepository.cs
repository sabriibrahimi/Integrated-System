using Domain.Models;

using Microsoft.EntityFrameworkCore.Metadata;
using Repository.Interface;
using EFCore.BulkExtensions;


namespace Repository.Implementation;

public class RoomRepository : IRoomRepository
{
    private readonly ApplicationDbContext _context;

    public RoomRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task BulkInsertOrUpdateRoomAsync(List<Room> rooms)
    {
        await _context.BulkInsertOrUpdateAsync(rooms);
    }

    public async Task BulkInsertOrUpdateConsultationAsync(List<Consultation> consultations)
    {
        await _context.BulkInsertOrUpdateAsync(consultations);
    }
}