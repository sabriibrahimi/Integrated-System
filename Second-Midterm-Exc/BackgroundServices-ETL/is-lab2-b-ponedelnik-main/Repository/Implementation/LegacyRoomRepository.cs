using Domain.ExternalModels;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;

namespace Repository.Implementation;

public class LegacyRoomRepository : ILegacyRoomRepository
{

    private readonly LegacyApplicationDbContext _legacyApplicationDbContext;

    public LegacyRoomRepository(LegacyApplicationDbContext legacyApplicationDbContext)
    {
        _legacyApplicationDbContext = legacyApplicationDbContext;
    }

    public async Task<List<Room>> GetRoomsModifiedSinceAsync(DateTime since)
    {
        var legacy = await _legacyApplicationDbContext.RoomDirectories
            .Where(x => x.UpdatedAt >= since).ToListAsync();

        return legacy.Select(x => new Room()
        {
            Id = GuidHelper.FromLegacyId("Room", x.RoomCode),
            Name = x.RoomName,
            Capacity = x.MaxCapacity
        }).ToList();
    }

    public async Task<List<Consultation>> GetConsultationModifiedSinceAsync(DateTime since)
    {
        var legacyConsultation = await _legacyApplicationDbContext.ConsultationSlots.AsNoTracking()
            .Where(c => c.UpdatedAt > since).ToListAsync();

        return legacyConsultation.Select(lc => new Consultation()
        {
            Id = GuidHelper.FromLegacyId("Consultation", lc.SlotId),
            StartTime = lc.SlotStart,
            EndTime = lc.SlotEnd,
            RoomId = GuidHelper.FromLegacyId("Room", lc.RoomCode)
        }).ToList();
}
}