using Domain.Models;
using Domain.Requests;

namespace Service.Interface;

public interface IInboundAttendanceService
{
     Task<Guid> CreateAsync(string rawPayload, Guid apiClientId);
     Task<InboundAttendanceEntry?> GetByIdAsync(Guid id);
     Task<List<InboundAttendanceEntry>> GetPendingBatchAsync(int take);

     Task UpdateAsync(InboundAttendanceEntry entry);
}