using Domain.Dto;
using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation;

public class AttendanceService : IAttendanceService
{
    private readonly IRepository<Attendance> _attendanceRepository;
    private readonly IConsultationService _consultationService;

    public AttendanceService(IRepository<Attendance> attendanceRepository, IConsultationService consultationService)
    {
        _attendanceRepository = attendanceRepository;
        _consultationService = consultationService;
    }

    public async Task<Attendance> GetByIdNotNullAsync(Guid id)
    {
        var result = await GetByIdAsync(id);
        if (result == null)
        {
            throw new InvalidOperationException($"Attendance with id {id} does not exists");
        }

        return result;
    }

    public async Task<Attendance?> GetByIdAsync(Guid id)
    {
        return await _attendanceRepository.GetAsync(
          selector: x=>x,
          predicate:x=>x.Id == id
        );
    }

    public async Task<List<Attendance>> GetAllAsync(string? dateAfter)
    {
        return await _attendanceRepository.GetAllAsync(
            selector: x=>x
        );
    }

    public async Task<Attendance> CreateAsync(AttendanceDto dto)
    {
        var attendance = new Attendance
        {
              ConsultationId = dto.ConsultationId,
              UserId = dto.UserId,
              RoomId = dto.RoomId,
              Comment = dto.Comment,
              Status = Status.Registered
        };

        var result = await _attendanceRepository.InsertAsync(attendance);
        await _consultationService.GetIncrementedAsync(dto.ConsultationId);
        return await GetByIdNotNullAsync(result.Id);
    }

    public async Task<Attendance> UpdateAsync(Guid id, AttendanceDto dto)
    {
        var attendanceToUpdate = await GetByIdNotNullAsync(id);

        attendanceToUpdate.ConsultationId = dto.ConsultationId;
        attendanceToUpdate.Comment = dto.Comment;
        attendanceToUpdate.RoomId = dto.RoomId;
        attendanceToUpdate.UserId = dto.UserId;

        return await _attendanceRepository.UpdateAsync(attendanceToUpdate);
    }

    public async Task<Attendance> DeleteByIdAsync(Guid id)
    {
        var attendanceToDelete = await GetByIdNotNullAsync(id);
        var consultation = await _consultationService.GetByIdNotNullAsync(attendanceToDelete.ConsultationId);

        if (consultation.RegisteredStudents > 0)
        {
            throw new InvalidOperationException("Attendance already registered in this consultation");
        }

        if (consultation.StartTime <= DateTime.Now.AddHours(1))
        {
            throw new InvalidOperationException("Deleting this registration is not allowed, try after one hour!");
        }

        await _consultationService.GetDecrementedAsync(consultation.Id);
        await _attendanceRepository.DeleteAsync(attendanceToDelete);
        return attendanceToDelete;
    }

    public async Task<PaginatedResult<Attendance>> GetPagedAsync(int pageNumber, int pageSize)
    {
        return await _attendanceRepository.GetAllPagedAsync(
          selector:x=>x,
          pageNumber: pageNumber,
          pageSize = pageSize
        );
    }

    public async Task<Attendance> UpdateReasonPathByIdAsync(Guid id, string path)
    {
        var attendance = await GetByIdNotNullAsync(id);
        attendance.CancellationReasonDocumentPath = path;
        return await _attendanceRepository.UpdateAsync(attendance);
    }

    public async Task<List<Attendance>> GetAllByConsultationIdAsync(Guid consultationId)
    {
        return await _attendanceRepository.GetAllAsync(
            selector: x=>x,
            predicate: x=>x.ConsultationId == consultationId,
            include:x=>x.Include(c=>c.User)
        );
    }

    public async Task<Attendance> MarkAsAbsentByIdAsync(Guid id)
    {
        var result = await GetByIdNotNullAsync(id);
        result.Status = Status.Absent; 
        return await _attendanceRepository.UpdateAsync(result);
    }
}