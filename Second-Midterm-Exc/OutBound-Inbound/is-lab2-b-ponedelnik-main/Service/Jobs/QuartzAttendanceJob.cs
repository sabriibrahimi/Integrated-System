using Service.Implementation;

namespace Service.Jobs;
using Quartz;

public class QuartzAttendanceJob : IJob
{

    private readonly InboundAttendanceProcessor _attendanceProcessor;

    public QuartzAttendanceJob(InboundAttendanceProcessor attendanceProcessor)
    {
        _attendanceProcessor = attendanceProcessor;
    }

    public async  Task Execute(IJobExecutionContext context)
    {
        await _attendanceProcessor.ProcessPendingAttendanceAsync();
    }
}