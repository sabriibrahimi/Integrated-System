using System.Collections.Specialized;
using Domain.Models;

namespace Repository.Interface;

public interface IRoomRepository
{
    Task BulkInsertOrUpdateRoomAsync(List<Room> rooms);
    Task BulkInsertOrUpdateConsultationAsync(List<Consultation> consultations);

}