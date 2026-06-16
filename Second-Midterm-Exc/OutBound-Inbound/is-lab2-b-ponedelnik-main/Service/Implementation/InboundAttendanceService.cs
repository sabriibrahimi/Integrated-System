using Domain.Enums;
using Domain.Models;
using Domain.Requests;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service.Interface;

namespace Service.Implementation;

public class InboundAttendanceService  : IInboundAttendanceService
{
    private readonly ApplicationDbContext _context;

    public InboundAttendanceService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> CreateAsync(string rawPayload, Guid apiClientId)
    {
        var entry =  new InboundAttendanceEntry()
        {
            Id = Guid.NewGuid(),
            RawPayload = rawPayload,
            ApiClientId = apiClientId,
            ReceivedAt = DateTime.UtcNow,
            Status = ProcessingStatus.Pending
        };

        _context.InboundAttendanceEntries.Add(entry);
        await _context.SaveChangesAsync();
        return entry.Id;
    }

    public async Task<InboundAttendanceEntry?> GetByIdAsync(Guid id)
    {
        return await _context.InboundAttendanceEntries
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<InboundAttendanceEntry>> GetPendingBatchAsync(int take)
    {
        return await _context.InboundAttendanceEntries
            .Where(x => x.Status == ProcessingStatus.Pending)
            .OrderBy(x => x.ReceivedAt)
            .Take(take)
            .ToListAsync();
    }

    public async Task UpdateAsync(InboundAttendanceEntry entry)
    {
        _context.InboundAttendanceEntries.Update(entry);
        await _context.SaveChangesAsync();
    }
}