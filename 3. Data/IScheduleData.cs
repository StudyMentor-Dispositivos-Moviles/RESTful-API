using _3._Data.Model;

namespace _3._Data;

public interface IScheduleData
{
    Schedule GetById(int id);
    Task<List<Schedule>> GetAll();
    bool Create(Schedule schedule);
    bool Delete(int id);
    bool Update(Schedule schedule, int id);
    Task<List<Schedule>> GetByTutorId(int id);
    Task<List<Schedule>> GetByStudentId(int id);
}