using _3._Data;
using _3._Data.Model;

namespace _2._Domain;

public class ScheduleDomain : IScheduleDomain
{
    private IScheduleData _scheduleData;

    public ScheduleDomain(IScheduleData scheduleData)
    {
        _scheduleData = scheduleData;
    }
    
    public bool Create(Schedule schedule)
    {
        if (string.IsNullOrWhiteSpace(schedule.TutorName) || string.IsNullOrWhiteSpace(schedule.Days) ||
            string.IsNullOrWhiteSpace(schedule.Time) || string.IsNullOrWhiteSpace(schedule.Price) || schedule.idTutor <= 0)
        {
            return false;
        }

        var existingSchedule = _scheduleData.GetByTutor(schedule.idTutor);
        if (existingSchedule != null)
        {
            return false;
        }

        return _scheduleData.Create(schedule);
    }

    public bool Delete(int id)
    {
        return _scheduleData.Delete(id);
    }
    
}