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

        return _scheduleData.Create(schedule);
    }
    public bool Update(Schedule schedule, int id)
    {
        return _scheduleData.Update(schedule, id);
    }
    public bool Delete(int id)
    {
        return _scheduleData.Delete(id);
    }
    
}