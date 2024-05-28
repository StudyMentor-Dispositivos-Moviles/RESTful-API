using _3._Data.Model;

namespace _3._Data;

public interface IScheduleData
{
    Schedule GetById(int id);
    Task<List<Schedule>> GetAll();
    Schedule GetByTutor(int tutorId);

    bool Create(Schedule schedule);
    bool Delete(int id);
}